# File name:    MichaelTang_Homework6b.py
# Due date:     Saturday, June 12, 2021
# Description:  Fix all the compilation, run time, and
#               logic errors from "SlotMachine Start.py".
#
#               Version 6b introduces a score file using a
#               dictionary to write and update player scores.

import random
import time
from FileReadWrite import *

# writeALine - write to a multiline file
# readALine - reading a file

# item "lemon" in list "reelList" is missing a beginning single quote
reelList = ['orange', 'orange', 'orange', 'lemon', 'lemon', 'lemon', \
              'plum', 'plum', 'plum', 'cherries', 'cherries', 'cherries', \
              'banana', 'banana', 'banana', 'bar', 'bar', 'Lucky 7']
nPicturesInReel = len(reelList) # nPicturesInReel should produce a number for the ending number
                                # of the 'randrange' call

DATA_FILE_PATH = 'SlotMachineData_MichaelTang3.txt'
namesAndScoresDict = {} # dictionary that will store data from DATA_FILE_PATH

# for the extra extra challenge - obtaining the player's name
playerName = input('What is your name? ')
print()

# check to see if a score data file exists
fileInDir = fileExists(DATA_FILE_PATH)

nCoins = 100  # new player gets 100 coins

def newPlayerPrintStatement():
    print('Welcome to the Slot Machine game ' + playerName + '!\nYou have', nCoins, 'coins to start.  Good luck.')

# Should a score data file exits, open it up and read to see if the person has played before
# If so, grab their score
if fileInDir:
    openedFile = openFileForReading(DATA_FILE_PATH)

    while True:  # read through file, build up list of lists
        savedDataString = readALine(openedFile)
        if savedDataString == False:  # no more lines
            break
        savedDataDict = savedDataString.split(',')  # split the key-value pair up by the comma

        # Save the dictionary to "namesAndScoresDict"
        # Casting the value automatically to an integer right now for simplicity

        name = savedDataDict[0] # get the name
        score = int(savedDataDict[1]) # get the previous score
        namesAndScoresDict[name] = score

    closeFile(openedFile)

    # See if this player is already in the list
    # If the player's name is found, get the score
    if playerName in namesAndScoresDict:
        nCoins = namesAndScoresDict[playerName]
        print('Hello ' + playerName + '!!!')
        print('Thanks for coming back to the Slot Machine game. Your current score is', nCoins, '.')
    else:
        newPlayerPrintStatement()
    
else:
    newPlayerPrintStatement()

print()

def payTable(myList):           # functions in Python are defined by keyword "def", not "function"
    picture1 = myList[0]
    picture2 = myList[1]
    picture3 = myList[2]
    if myList.count(picture1) == 3:
        if picture1== 'Lucky 7':
            nCoinsWon = 500
        elif picture1== 'bar':
            nCoinsWon = 100
        else:
            nCoinsWon = 10

    else:
        # second statement originally showing picture2 equalling itself
        # instead, it should see if picture2 is the same as picture 3,
        # which is another logical combination of two slots having the same "image"
        if (picture1 == picture2) or (picture2 == picture3) or (picture1 == picture3):
            nCoinsWon = 2
        else:
            nCoinsWon = 0

    return nCoinsWon # function should return the number of coins won, as indicated by the function call

while True:

    bet = input('How many coins do you want to bet (defaults to 1, enter 0 to quit): ')

    # put either single or double quotes around the 0 because the comparison should be to a string,
    # which is how Python stores a value from an "input" statement
    if bet == '0':
        break

    elif bet == '':
        bet = 1

    try:
        bet = int(bet)  # cast the variable "bet" as an integer so math can be done on it
    except:
        print('Sorry, \'' + bet + '\' is not a valid bet.')
        print('Your entry must be a positive integer. Please try again.')
        print()
        continue

    if bet < 0:
        print('Sorry, \'' + '{:d}'.format(bet) + '\' is not a valid bet.')
        print('Your entry must be a positive integer. Please try again.')
        print()
        continue

    elif bet > int(nCoins):
        print('Sorry, you do not have that many coins, you only have: ' + '{:d}'.format(nCoins))
        print()
        continue
    
    resultList = []
    for spin in range(3):
        thisReelIndex = random.randrange(0, nPicturesInReel)
        thisPicture = reelList[thisReelIndex]
        resultList.append(thisPicture)

    print( 'Spinning ... ')
    print()
    time.sleep(.5)
    print( '     ', resultList[0])
    time.sleep(.5)
    print( '     ', resultList[1])
    time.sleep(.5)
    print( '     ', resultList[2])
    print( )
        
    nCoins = int(nCoins) - bet   # nCoins should show the amount after having placed the bet...minus sign instead of plus
    payOut = bet * payTable(resultList)

    if payOut == 0:         # this is comparing to see if the payout is 0, not assigning it to 0 - therefore, should be a double equals sign operator
        print( 'Sorry - you lose.')
    else:
        print( 'You won', payOut, 'coins.  Cha-ching!')
        if payOut > 50:
            print( '                         WOOOOO  HOOOOOOOOOOO!!!!')
            
    nCoins = nCoins + payOut

    # format variable "nCoins" as an integer because it will not print correctly under the stock print command
    # also - added a few spaces were added for clearer display
    print( 'You now have ' + '{:d}'.format(nCoins) + ' coins.') 
    print()

print( 'Sorry to see you go.  Come back again soon.')

# ending the game - write the name and score to the data file
openedFile = openFileForWriting(DATA_FILE_PATH)

# Update the player's score
namesAndScoresDict[playerName] = nCoins # update the score to the player already in the dictionary
    
# This for loop will iterate through the dictionary and write each line to the file
for entry in namesAndScoresDict:
    if entry == '':
        break

    textToWrite = ''
    textToWrite = entry + ',' + str(namesAndScoresDict[entry])
    writeALine(openedFile, textToWrite)

openedFile.close()
