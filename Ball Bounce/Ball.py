import pygame
from pygame.locals import *
import random

# BALL CLASS 
class Ball():

    def __init__(self, window, windowWidth, windowHeight):
        self.window = window  # remember the window, so we can draw later
        self.windowWidth = windowWidth
        self.windowHeight = windowHeight

        self.ballImage = pygame.image.load("images/ball.png")
        # A rect is made up of [x, y, width, height]
        ballRect = self.ballImage.get_rect()
        self.width = ballRect[2]
        self.height = ballRect[3]
        self.maxWidth = windowWidth - self.width
        self.maxHeight = windowHeight - self.height
        
        # Pick a random starting position 
        self.x = random.randrange(0, self.maxWidth)
        self.y = random.randrange(0, self.maxHeight)

        # Choose a random speed in the y direction
        self.xSpeed = random.randrange(1, 4)
        self.ySpeed = random.randrange(1, 4)

    def update(self):
        # check for hitting a wall.  If so, change that direction
        if (self.x < 0) or (self.x > self.maxWidth):
            self.xSpeed = -self.xSpeed

        if (self.y < 0) or (self.y > self.maxHeight):
            self.ySpeed = -self.ySpeed

        # update the balls x and y, based on the speed in two directions
        self.x = self.x + self.xSpeed
        self.y = self.y + self.ySpeed

    # Extra challenge - method added
    #
    # The "reverseDirection" method will reverse the drection
    # of the Ball object when its "rectangle" is clicked
    def reverseDirection(self, mouseLoc):
        ballRectMod = pygame.Rect(self.x, self.y, self.width, self.height)
        if ballRectMod.collidepoint(mouseLoc):

            self.xSpeed = -self.xSpeed
            self.ySpeed = -self.ySpeed

            self.x = self.x + self.xSpeed
            self.y = self.y + self.ySpeed

    def draw(self):
        self.window.blit(self.ballImage, (self.x, self.y))
