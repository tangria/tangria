#  Balloon base class and 3 subclasses

import pygame
import random
from pygame.locals import *
import pygwidgets
from BalloonConstants import *
from abc import ABC, abstractmethod

class Balloon(ABC):

    popSoundLoaded = False
    popSound = None  # Load when first balloon is created

    @abstractmethod
    def __init__(self, window, maxWidth, maxHeight, ID,
                 oImage, size, nPoints, speedY):
        self.window = window
        self.ID = ID
        self.balloonImage = oImage
        self.size = size
        self.nPoints = nPoints
        self.speedY = speedY
        if not Balloon.popSoundLoaded:  # Load first time only
            Balloon.popSoundLoaded = True
            Balloon.popSound = pygame.mixer.Sound('sounds/balloonPop.wav')

        balloonRect = self.balloonImage.getRect()
        self.width = balloonRect.width
        self.height = balloonRect.height
        # Position so balloon is within the width of the window,
        # but below the bottom
        self.x = random.randrange(maxWidth - self.width)
        self.y = maxHeight + random.randrange(75)
        self.balloonImage.setLoc((self.x, self.y))

    def clickedInside(self, mousePoint):
        myRect = pygame.Rect(self.x, self.y, self.width, self.height)
        if myRect.collidepoint(mousePoint):
            Balloon.popSound.play()
            return True, self.nPoints # True here means it was hit
        else:
            return False, 0  # Not hit, no points

    def update(self):
        self.y = self.y - self.speedY   # update y position by speed
        self.balloonImage.setLoc((self.x, self.y))
        if self.y < -self.height:     # Off the top of the window
            return BALLOON_MISSED
        else:
            return BALLOON_MOVING

    def draw(self):
        self.balloonImage.draw()

    def __del__(self):
        print(self.size, 'Balloon', self.ID, 'is going away')


class BalloonSmall(Balloon):
    balloonImage = pygame.image.load('images/redBalloonSmall.png')
    def __init__(self, window, maxWidth, maxHeight, ID):
        oImage = pygwidgets.Image(window, (0, 0),
                                  BalloonSmall.balloonImage)
        super().__init__(window, maxWidth, maxHeight, ID,
                         oImage, 'Small', 30, 3.1)

class BalloonMedium(Balloon):
    balloonImage = pygame.image.load('images/redBalloonMedium.png')
    def __init__(self, window, maxWidth, maxHeight, ID):
        oImage = pygwidgets.Image(window, (0, 0),
                                  BalloonMedium.balloonImage)
        super().__init__(window, maxWidth, maxHeight, ID,
                         oImage, 'Medium', 20, 2.2)

class BalloonLarge(Balloon):
    balloonImage = pygame.image.load('images/redBalloonLarge.png')
    def __init__(self, window, maxWidth, maxHeight, ID):
        oImage = pygwidgets.Image(window, (0, 0),
                                  BalloonLarge.balloonImage)
        super().__init__(window, maxWidth, maxHeight, ID,
                         oImage, 'Large', 10, 1.5)

        if not Balloon.popSoundLoaded:  # Load first time only
            Balloon.popSoundLoaded = True
            Balloon.popSound = pygame.mixer.Sound('sounds/balloonPop.wav')

class MegaBalloon(Balloon):
    popSqueakLoaded = False
    popSqueak = None  # Load when first megaballoon is created

    # used only if a single MegaBallon image is used (versus an "ImageCollection")
    ##balloonImage = pygame.image.load('images/megaBalloon.png')

    def __init__(self, window, maxWidth, maxHeight, ID):

        oImage = pygwidgets.ImageCollection(window, (0, 0), \
                    {'image0': 'megaBalloon3.png', 'image1': 'megaBalloon2.png', \
                     'image2': 'megaBalloon1.png'}, 'image0', path='images/')

        # this "oImage" object will be used only when a
        # single MegaBallon image is being used
        ##oImage = pygwidgets.Image(window, (0, 0), MegaBalloon.balloonImage)

        self.balloonCounter = 0     # initial counter value for the mega balloon

        # call to the inherited __init__() from the Balloon base class
        super().__init__(window, maxWidth, maxHeight, ID,
                        oImage, 'Mega', 50, 4)

        if not MegaBalloon.popSqueakLoaded:  # Load first time only
            MegaBalloon.popSqueakLoaded = True
            MegaBalloon.popSqueak = pygame.mixer.Sound('sounds/balloonSqueak.wav')

    def clickedInside(self, mousePoint):
        myRect = pygame.Rect(self.x, self.y, self.width, self.height)
        if myRect.collidepoint(mousePoint):
            self.balloonCounter = self.balloonCounter + 1
            if self.balloonCounter == 3:
                MegaBalloon.popSound.play()
                return True, self.nPoints # True here means it was hit
            else:
                # use the appropriate balloon image based on how many clicks
                # are still required to pop the MegaBalloon
                self.balloonImage.replace('image' + str(self.balloonCounter))

                MegaBalloon.popSqueak.play()
                return True, 0  # The balloon was hit, but no points are awarded since it takes
                                # three clicks to pop a mega balloon - therefore, enter "0" points
                                # so this balloon is not removed, and the balloon popped counter
                                # along with the score is not updated
        else:
            return False, 0  # Not hit, no points