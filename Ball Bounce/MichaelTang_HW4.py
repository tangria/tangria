# File name:    MichaelTang_HW4.py
# Due date:     Thursday, July 29, 2021
# Description:  Change the program so that it starts without showing any balls,
#               create a new ball when a "b" is pressed, and create a Drop.py
#               file containing a "Drop" class.

# pygame demo using Ball class, bounce many balls


# 1 - Import packages
import pygame
from pygame.locals import *
import sys
import random
from Ball import *  # bring in the Ball class code
from Drop import *
from SimpleText import * # bring in the SimpleText code

# 2 - Define constants
BLACK = (0, 0, 0)
WHITE = (255, 255, 255)
WINDOW_WIDTH = 640
WINDOW_HEIGHT = 480
FRAMES_PER_SECOND = 30

# 3 - Initialize the world
pygame.init()
window = pygame.display.set_mode((WINDOW_WIDTH, WINDOW_HEIGHT))
clock = pygame.time.Clock()  # set the speed (frames per second)

# 4 - Load assets: image(s), sounds, etc.
oDisplay = SimpleText(window, (100, 450), \
            'Press b for a new Ball, Press d for a new Drop', WHITE)

# 5 - Initialize variables
ballList = []   # list containing Ball objects
dropList = []   # list containing Drop objects

# 6 - Loop forever
while True:
    
    # 7 - Check for and handle events
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            pygame.quit()
            sys.exit()

        # see if user pressed a valid key ("b" or "d")
        # reference - https://www.pygame.org/docs/ref/key.html
        elif event.type == pygame.KEYDOWN:

            # create a brand new Ball or Drop object with the required
            # arguments when the "b"/"d" key is pressed
            if event.key == pygame.K_b:
                oBall = Ball(window, WINDOW_WIDTH, WINDOW_HEIGHT)
                ballList.append(oBall)  # append the new ball to the list of balls  

            elif event.key == pygame.K_d:
                oDrop = Drop(window, WINDOW_WIDTH, WINDOW_HEIGHT)
                dropList.append(oDrop)  # append the new drop to the list of drops

        # Extra challenge portion
        #
        # This "for" loop will evaluate all the objects in the ballList list and see
        # if a moust event is applied to any of the objects and if so, the "reverseDirection"
        # method will be called in the Ball class to reverse the x and y direction for the
        # ball that was clicked

        if event.type == pygame.MOUSEBUTTONUP:
            
            # "reversed" iterates through in reverse order so that it picks up the latest (highest layer) object
            for oBall in reversed(ballList):    
                reversedOneBall = oBall.reverseDirection(event.pos)
                if reversedOneBall:
                    break

    # 8 - Do any "per frame" actions
    for oBall in ballList:
        oBall.update()  # tell each ball to update itself

    for oDrop in dropList:
        oDrop.update()

   # 9 - Clear the screen before drawing it again
    window.fill(BLACK)
    
    # 10 - Draw the screen elements
    for oBall in ballList:
        oBall.draw()   # tell each ball to draw itself

    for oDrop in dropList:
        oDrop.draw()

    oDisplay.draw()

    # 11 - Update the screen
    # now show it in the real physical window
    pygame.display.update()

    # 12 - Slow things down a bit
    clock.tick(FRAMES_PER_SECOND)  # make PyGame wait the correct amount
