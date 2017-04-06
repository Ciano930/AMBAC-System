import socket

backlog = 1
size = 1024
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind(('192.168.1.10', 50001))
s.listen(backlog)
try:
    print ("is waiting")
    client, address = s.accept()

    while 1:
        data = client.recv(size)
        if data:
            print (data)

except:
    print("closing socket")
    client.close()
    s.close()
