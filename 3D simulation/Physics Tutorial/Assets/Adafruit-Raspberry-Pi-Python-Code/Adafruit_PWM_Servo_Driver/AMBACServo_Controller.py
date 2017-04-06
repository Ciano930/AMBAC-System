#!/usr/bin/python

from Adafruit_PWM_Servo_Driver import PWM
import time
import socket


# ===========================================================================
#Main Servo Controller code.
# ===========================================================================

print "Starting"

# Initialise the PWM device using the default address
pwm = PWM(0x40)
# Note if you'd like more debug output you can instead run:
pwm = PWM(0x40, debug=True)

servoMin = 150  # Min pulse length out of 4096
servoMax = 600  # Max pulse length out of 4096

#Servo pins = [0], starting angle = [1], angle limits+ = [2] angle limits- =[3] left side = 1[4] right side = 0[4] abs = 2[4]
# ===========================================================================
ankleRUpDown = [6,5,45,-50,0,5]
ankleRSideSide = [7,10,40,-15,0,10]
ankleLUpDown = [0,-10,50,-45,1,-10]
ankleLSideSide = [1,0,15,-40,1,0]

kneeR = [8,-80,0,-80,0,-80]#errors occuring on right side
kneeL = [2,80,80,0,1,80]

thighR = [9,0,30,-30,0,0]
thighL = [3,0,30,-30,1,0]

hipRUpDown = [11,3,40,-80,0,3]
hipROutIn = [10,0,70,-5,0,10]
hipLUpDown = [5,-3,80,-40,1,-3]
hipLOutIn = [4,0,15,-60,1,0]#needs ankle extensions to go further will add if proves to be issue.

abSideSide = [12,-3,15,-21,2,-3]
abForwardBack = [15,0,12,-45,2,0]

#now lets make these into a list in the correct pin order
listOfJoints = [ankleLUpDown, ankleLSideSide, kneeL, thighL, hipLOutIn, hipLUpDown, ankleRUpDown, ankleRSideSide, kneeR, thighR, hipROutIn, hipRUpDown, abSideSide, abForwardBack]
#testing list
print("ListOfjoints pin 0, 8, 15:")
print(listOfJoints[0])
print(listOfJoints[8])
print(listOfJoints[13])


def setServoPulse(channel, pulse):
  pulseLength = 1000000                  # 1,000,000 us per second
  pulseLength /= 60                       # 60 Hz
  print "%d us per period" % pulseLength
  pulseLength /= 4096                     # 12 bits of resolution
  print "%d us per bit" % pulseLength
  pulse *= 1000
  pulse /= pulseLength
  pwm.setPWM(channel, 0, pulse)

pwm.setPWMFreq(60)                        # Set frequency to 60 Hz

def setAngle(channel, angle, delta=170):
    #I think this may be causing issues for my motors as if they aren't quick enough they may be missing this delay timer.
    delay = max(delta * 0.003, 0.03)        # calculate delay
    zero_pulse = (servoMin + servoMax) / 2  # half-way == 0 degrees
    pulse_width = zero_pulse - servoMin     # maximum pulse to either side
    pulse = zero_pulse + (pulse_width * angle / 80)
    print "angle=%s pulse=%s" % (angle, pulse)
    pwm.setPWM(channel, 0, int(pulse))
    time.sleep(delay)  # sleep to give the servo time to do its thingfrom Adafruit_PWM_Servo_Driver import PWM


def resetMotors():
  for list in listOfJoints:
    setAngle(list[0], list[1], 0)
  print ("Motors reset")

#now we reset the motors and prepare for the socket data
resetMotors()

#un-necissary update caller
"""
def runUpdate():
  for list in listOfJoints:
    setAngle(list[0], list[5], 0)
  print("updated")
"""

#============================================================================
#Now we ensure the update will not exceed the motor of our joint
#============================================================================
def motorUpdate(pin, angle):
  try:
    print("running update")
    print("Angle" + str(angle))
    print("pin:"+str(pin))
    if(pin == 15):#to catch odd pin as board is broken on pins 13 and 14
      joint = listOfJoints[13]
    else:
      try:
        print("listOfJoints[pin]:"+ str(listOfJoints[int(pin)]))
        joint = listOfJoints[int(pin)]
        print("Joint pin:" +str(joint[0]))
      except:
        print("error assigning Joint")
  except:
    print("error checkimg if pin == 15")
  if(joint[4] == 1):
    #right side
    angle = 0-angle #angle is inverted on right side
    print("angle after negation" + str(angle))
  #now lets check limits
  if(angle < joint[2]):
    if(angle > joint[3]):
      print("Within limits updating")
      #joint[5] = joint[1] - angle
      joint[5] = angle
      listOfJoints[pin] = joint
      try:
        setAngle(joint[0], joint[5], 0)
      except:
        print("update error")
    else:
      print("Limit breached")
  else:
    print("Limit breached")

# ===========================================================================
#Now that we have the motors managed we need to set up our socket
# ===========================================================================

address = '192.168.1.9'
port = 50002
backlog = 1
firstMessage = True
size = 4096
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind((address, port))
s.listen(backlog)
try:
    print ("is waiting on address:" + address + ", " + str(port))
    client, address = s.accept()

    while 1:
        data = client.recv(size)
        if data:
          if firstMessage: #this will catch the first message for us and ensure we do not close the socket accidentally
            print("first messsage" +data)
            firstMessage=False
          else:
            #Issue with the data arriving late means we need a way to discard half data
            messages = data.split("_")
            for message in messages:
              print("message raw:" +message)
              if(message == ""):
                print("NULL message:" + message)
              else:
                messageVals = message.split(",")
                notNull = True
                for messageVal in messageVals:
                  if(messageVal == ""):
                    print("NULL value:" + messageVal)
                    notNull = False
                  else:
                    notNull = True
                if notNull:
                  try:
                    pin = int(messageVals[0])
                    angle = int(messageVals[1])
                    try:
                      print("Angle" + str(angle))
                      print("pin:"+str(pin))
                    except:
                      print("angle error")
                    try:
                      motorUpdate(pin, angle)#error on right side leg, Fixed was a print issue
                    except:
                      print("Motor update error")
                  except:
                    print("data error Skipping")
                else:
                  print("Skipping set")

except:
    print("Error closing socket")
    client.close()
    s.close()


"""
    Servo Pins
    Left Leg
        ankle up down= 0
        ankle side side = 1
        knee = 2
        thigh swivel = 3
        Hip out in = 4
        hip up down = 5
    Right Leg
        ankle up down= 6
        ankle side side = 7
        knee = 8
        thigh swivel = 9
        Hip out in = 10
        hip up down = 11
    Spine
        Side Side = 12
        Forward Back = 13
"""

"""

    Knee L = 80 degrees,
    Knee R = -80 degrees,
    Hip up down L&R = 0 degrees,
    Ab side side = 0 degrees,
    ab forward back = 0 degrees,
    ankle side side R = 10 degrees,
    ankle side side L = 0 degrees,
    ankle forward, back L&R = 0 degrees,
    Thigh swivel L&R = 0 degrees,
"""
