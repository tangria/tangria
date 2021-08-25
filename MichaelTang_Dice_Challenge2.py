# File name:    MichaelTang_Dice_Challenge2.py
# Due date:     Thursday, July 22, 2021
# Description:  This is an extra extension of the "Dice" homework.
#               Not only will will print an integer number of
#               asterisks to represent the percentage instead
#               of a text-only output of how many times each "bin"
#               appeared within that round, but it will also break
#               down the details of the the totals of the die
#               combinations rolled that resulted in each bin value.

# Dice - count totals in user-defined number of rounds

import random

class Bin():
    def __init__(self, binIdentifier):
        # This method runs whenever you create a Bin object
        self.binIdentifier = binIdentifier
        self.oDict = {} # initialize the dictionary

    def reset(self):
        # This is called when you start or restart
        self.count = 0 # counter for each bin is set to 0
        self.oDict = {} # reset the object's dictionary

    def increment(self, firstDie, secondDie):
        # Called when the roll total is the value of the binIdentifier        
        self.count = self.count + 1

        tempString = str(firstDie) + ' and ' + str(secondDie)
        tempReverseString = str(secondDie) + ' and ' + str(firstDie)

        # checking to see if key exists in dictionary
        # if tempString in self.oDict.keys()
        if tempString in self.oDict:
            self.oDict[tempString] = self.oDict[tempString] + 1

        # the key "might" exist already in the dictionary, but in reverse order
        # from the aforementioned if statement
        # should this be the case, then increment based on the "reversed string"
        # otherwise, add a new key-value pair in the dictionary
        else:
            if tempReverseString in self.oDict:
                self.oDict[tempReverseString] = self.oDict[tempReverseString] + 1
            else:
                self.oDict[tempReverseString] = 1

    def show(self, nRoundsDone):
        if self.binIdentifier < 2:
            return
        
        # Called at the end to show the contents of this bin
        count = str(self.count)
        percentBin = (self.count/nRoundsDone) * 100
        percentBin = int(percentBin)
        printStars = '*' * percentBin
        percentBin = str(percentBin)
        
        print(str(self.binIdentifier) + ': ' + printStars + ' ' + percentBin + '%(' + count + ')')
        for i in self.oDict:
            
            # perform string concatenations & conversions for the print statement
            # ahead of time so that the print statement is simple to put together
            rollComboCount = self.oDict[i]
            rollComboCount = str(rollComboCount)
            percentCombo = (self.oDict[i]/self.count) * 100
            percentCombo = int(percentCombo)
            percentComboStr = str(percentCombo)
            
            print('        ' + str(i) + ': ' + rollComboCount + ' = ' + percentComboStr + ' %')

# Build a list of Bin objects            
binList = []  # start off as the empty list

# Here, you need to write a loop that runs 13 times (0 to 12)
# In the loop create a Bin object, and store the object in the list of Bins
# (We won't use binList[0] or binList[1])

# using a "for" loop, create Bin objects 0-12
# each time an Bin object is created, store it in list "binList"
for i in range(13):
    oBin = Bin(i)
    binList.append(oBin) # add the object to the list so that the list will have a total of 12 objects

while True:
    nRounds = input('How many rounds do you want to do? (or Enter to exit): ')
    if nRounds == '':
        break
    nRounds = int(nRounds)

    # Tell each bin object to reset itself
    for oBin in binList:
        oBin.reset()
        
    # For each round (build a loop):
    #     roll two dice
    #     calculate the total
    #     and tell the appropriate bin object to increment itself

    for i in range(1,nRounds):
        # each dice's range is from 1-6
        dice1 = random.randrange(1,7)
        dice2 = random.randrange(1,7)
        diceSum = dice1 + dice2

        binList[diceSum].increment(dice1, dice2)
        
    print()
    print('After', nRounds, 'rounds:')
    # Tell each bin to show itself
    for oBin in binList:
        oBin.show(nRounds)
    print()

print('OK bye')
