oldS = "          U heeft geen stoelen gereserveerd        "
j = 0
for i in oldS:
    j = j + 1
print(j)
print("[" + oldS + "]")

while len(oldS) < 72:
    oldS = oldS + " "
    if len(oldS) < 72:
        oldS = " " + oldS
print("[" + oldS + "]")

j = 0
for i in oldS:
    j = j + 1
print(j)