__author__ = 'Mattias'
import time
from neopixel import *


def lerp(arg1, arg2, t):
    return (1-t)*arg1 + t*arg2


def fraction_range(start, end, step):
    while start <= end:
        yield start
        start += step


def colour_sweep(strip, source, target, t):
        interval = t/10.0
        for x in fraction_range(0.1, 1, 0.1):
		#Loop through each floor
		index = 0
		for floor in sorted(source):
			fromColour = source[floor]
			toColour = target[floor]
			if (fromColour[0] != toColour[0]) or (fromColour[1] != toColour[1]) or (fromColour[2] != toColour[2]):
				print index, "Sweeping from ", fromColour, " to ", toColour
		                red = round(lerp(fromColour[0], toColour[0], x))
		                green = round(lerp(fromColour[1], toColour[1], x))
		                blue = round(lerp(fromColour[2], toColour[2], x))
				strip.setPixelColor(index, Color(int(red), int(green), int(blue)))
			index += 1
		
		strip.show()
		time.sleep(interval)
