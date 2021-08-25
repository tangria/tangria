# File name: Drop.py

import pygame
from pygame.locals import *
import random

# DROP CLASS 
class Drop():

    def __init__(self, window, windowWidth, windowHeight):
        self.window = window  # remember the window, so we can draw later
        self.windowWidth = windowWidth
        self.windowHeight = windowHeight

        self.dropImage = pygame.image.load("images/drop.png")
        # A rect is made up of [x, y, width, height]
        dropRect = self.dropImage.get_rect()
        self.width = dropRect[2]
        self.height = dropRect[3]
        self.maxWidth = windowWidth - self.width # 640 - width of image
        self.maxHeight = windowHeight - self.height # 480 - height of image 
        
        # Pick a random starting x position for the Drop object
        self.x = random.randrange(0, self.maxWidth)

        # start the drop at the top means that the
        # starting value of y is 0
        #
        # and since the drop will only be moving in the y direction,
        # the speed for x should be 0
        self.y = 0
        #self.y = -self.height

        # Choose a random speed for the y direction
        self.ySpeed = random.randrange(1, 4)
        #self.ySpeed = random.randrange(3, 7)

    def update(self):
        # check for hitting the bottom. If so, have the drop
        # start at the very top in the same x direction for
        # which the drop was initially falling from

        if (self.y < 0) or (self.y > self.maxHeight):
            self.y = 0

        # update the drop x and y, based on the speed of the y direction
        self.y = self.y + self.ySpeed

    def draw(self):
        self.window.blit(self.dropImage, (self.x, self.y))
