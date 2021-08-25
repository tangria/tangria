# File name:    Michael_Tang_Rectangle.py
# Due date:     Thursday, July 15, 2021
# Description:  Modify file "Square Start.py" to create
#               a Rectangle object, set the horizontal/vertical/
#               corner/size (of the edges), get the value of
#               the size and area, and present a text-based
#               representation of the rectangle using the current
#               horizontal, vertical, and corner characters.
#
#               This will be slightly different than a square because
#               the program needs to consider that the height and width
#               of a rectangle may have different lengths.

# Rectangle class - text based
# Allows you to change set initial data, then change the data, and show the rectangle as text
# Because of fonts, rectangles will probably not show as exactly rectangles

class Rectangle():
    """Represents a rectangle
    """

    # Modification 1 - instead of just "size", include height and width parameters
    # in the __init__ method and create instance variables for both of them
    def __init__(self, height, width, hChar, vChar, cornerChar):
        ''' Initializes a rectangle
        Pass in the size of the height/width, the character to used for the horizontal edge, vertical edge, and corners.
        '''
        self.height = height
        self.width = width
        self.hChar = hChar
        self.vChar = vChar
        self.cornerChar = cornerChar

    def show(self):
        ''' Print the rectangle in text using the horizontal edge, vertical edge, and corner characters
        Use a space (' ') for all characters not on an edge
        '''       

        # counter for the while statement to loop through the rows (width) of the rectangle
        counter = 0
        
        # simplifies the calculation for the number of characters needed between the edges/corners
        middleSpaceFillerSize = self.height - 2

        # Modification 2 - using the rectangle's width as the basis, iterate through
        # the width's length to figure out if that "width line" needs to have 
        # horizontal/corner characters or vertical/space characters
        while counter < self.width:
            if counter in (0, self.width - 1): # these are the corners, so print the corner character when 0 or (self.size-1)
                print(self.cornerChar + (self.hChar * middleSpaceFillerSize) + self.cornerChar)
            else:
                print(self.vChar + (' ' * middleSpaceFillerSize) + self.vChar)
            counter = counter + 1

    # Modificaiton 3 - method "getSize" needs to be broken up into separate get functions for height and width
    def getHeight(self):
        ''' Returns the legnth of the Rectangle '''
        return self.height

    def getWidth(self):
        ''' Returns the width of the Rectangle '''
        return self.width        

    def setHorizontalChar(self, newHChar):
        ''' Set a new horizontal character '''
        self.hChar = newHChar
        
    def setVerticalChar(self, newVChar):
        ''' Set a new vertical character '''
        self.vChar = newVChar

    def setCornerChar(self, newCornerChar):
        ''' Set a new corner character '''
        self.cornerChar = newCornerChar

    # Must add THREE additional methods: setHeigth, setWidth and getArea

    # Modification 4 - method "setSize" needs to be broken up into separate set methods for height and width
    def setHeight(self, height):
        self.height = height

    def setWidth(self, width):
        self.width = width

    # Modification 5 - modify the area calculation so that it is the product of the Rectangle object's height & width
    def getArea(self):
        return self.height * self.width


# Test code
# Create a rectangle of size 5 by 3
oRectangle1 = Rectangle(5, 3, '-', '|', '*')  # pass in size, horizonal, vertical, and edge characters
oRectangle1.show()
print('Size is:', oRectangle1.getHeight(), " by", oRectangle1.getWidth(), " area is:", oRectangle1.getArea())

# Create another rectangle of size 10 by 8
oRectangle2 = Rectangle(10, 8, '-', '|', '*')
oRectangle2.show()
print('Size is:', oRectangle2.getHeight(), " by", oRectangle2.getWidth(), " area is:", oRectangle2.getArea())

# Tell the first square to modify its data
oRectangle1.setHeight(7)
oRectangle1.setWidth(9)
oRectangle1.setHorizontalChar('^')
oRectangle1.setVerticalChar('?')
oRectangle1.setCornerChar('$')
oRectangle1.show()
print('Size is:', oRectangle1.getHeight(), " by", oRectangle1.getWidth(), " area is:", oRectangle1.getArea())
print()


# Add code here to ask the user questions, and create and show a new Square based on the answers
sizeHeight = input("What is the (integer) HEIGHT of the rectangle? ")
sizeHeight = int(sizeHeight)

sizeWidth = input("What is the (integer) WIDTH of the rectangle? ")
sizeWidth = int(sizeWidth)

horizontalInput = input("What should we use for the horizontal character? ")
verticalInput = input("What should we use for the vertical character? ")
cornerInput = input("What should we use for the corner character? ")

# Create an instance of a Rectangle class
oRectangle3 = Rectangle(sizeHeight, sizeWidth, horizontalInput, verticalInput, cornerInput)
oRectangle3.show()
print('Size is:', oRectangle3.getHeight(), " by", oRectangle3.getWidth(), " area is:", oRectangle3.getArea())
print()
