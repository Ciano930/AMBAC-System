#!/usr/bin/python

from Adafruit_PWM_Servo_Driver import PWM
import time

# ===========================================================================
# Example Code
# ===========================================================================

print "Starting"

# Initialise the PWM device using the default address
pwm = PWM(0x40)
# Note if you'd like more debug output you can instead run:
pwm = PWM(0x40, debug=True)

servoMin = 150  # Min pulse length out of 4096
servoMax = 600  # Max pulse length out of 4096

def setServoPulse(channel, pulse):
  pulseLength = 1000000                   # 1,000,000 us per second
  pulseLength /= 60                       # 60 Hz
  print "%d us per period" % pulseLength
  pulseLength /= 4096                     # 12 bits of resolution
  print "%d us per bit" % pulseLength
  pulse *= 1000
  pulse /= pulseLength
  pwm.setPWM(channel, 0, pulse)

pwm.setPWMFreq(60)                        # Set frequency to 60 Hz

def setAngle(channel, angle, delta=170):
    """
        Sets angle of servo (approximate).
        Pin channel servo is attached to (0-15) minus 13 and 14 due to solder error
        Àngle of servos starting off of center, ( -80 through 80)
        Angle changed from past position. Used to calculate
        delay, since rotating thru a larger arc takes longer
        than a shorter arc.
    """
    #I think this may be causing issues for my motors as if they aren't quick enough they may be missing this delay timer.
    delay = max(delta * 0.003, 0.03)        # calculate delay
    zero_pulse = (servoMin + servoMax) / 2  # half-way == 0 degrees
    pulse_width = zero_pulse - servoMin     # maximum pulse to either side
    pulse = zero_pulse + (pulse_width * angle / 80)
    print "angle=%s pulse=%s" % (angle, pulse)
    pwm.setPWM(channel, 0, int(pulse))
    time.sleep(delay)  # sleep to give the servo time to do its thingfrom Adafruit_PWM_Servo_Driver import PWM

getPin = True
angle = 0
pin = 0
while(True):

    if getPin:
        print("please enter pin ( 0 = 1, 15 = 16 etc) to test")
        pin = input()

        if pin:
            try:
                pin = int(pin) - 1
                setAngle(pin, angle, 0)
                print("pin is now at zero")
                print(" increase angle with 1")
                print(" decrease angle with 2")
                print(" update the motor with 3")
                print(" reset with 4")
                getPin = False
            except:
                print("Please use an integer value")
                getPin = True
    else:
        print("ready for input")
        inputVal = input()
        if inputVal:
            inputVal = int(inputVal)
            if (inputVal == 1):
                angle = angle + 1
                print("angle: " + str(angle))
            elif(inputVal == 2):
                angle = angle - 1
                print("angle: " + str(angle))
            elif(inputVal == 3):
                print("Updating Motor")
                setAngle(pin, angle, 0)
            elif(inputVal == 4):
                angle = 0
                print("angle: " + str(angle))

            else:
                print("That is not a known value")
"""
    limits
    Knee L = 80 degrees,
    Knee R = -80 degrees,
    Hip up down L&R = 0 degrees,
    Ab side side = 0 degrees,
    ab forward back = 0 degrees,
    ankles L&R = 0 degrees,
    Thigh swivel L&R = 0 degrees,
  """
