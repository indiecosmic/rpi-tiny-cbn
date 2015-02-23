__author__ = 'Mattias'
import time
from utils import coloursweep
from colourbynumbers import api
from neopixel import *


# LED strip configuration:
LED_COUNT      = 10      # Number of LED pixels.
LED_PIN        = 18      # GPIO pin connected to the pixels (must support PWM!).
LED_FREQ_HZ    = 800000  # LED signal frequency in hertz (usually 800khz)
LED_DMA        = 5       # DMA channel to use for generating signal (try 5)
LED_BRIGHTNESS = 255     # Set to 0 for darkest and 255 for brightest
LED_INVERT     = False   # True to invert the signal (when using NPN transistor level shift)


if __name__ == '__main__':
	strip = Adafruit_NeoPixel(LED_COUNT, LED_PIN, LED_FREQ_HZ, LED_DMA, LED_INVERT, LED_BRIGHTNESS)
	strip.begin()

	floors = api.get_floor_colours()
	index = 0
	for floor in sorted(floors):
		colour = floors[floor]
		print index, colour
		strip.setPixelColor(index, Color(colour[0], colour[1], colour[2]))
		print index, strip.getPixelColor(index)
		strip.show()
		time.sleep(0.5)
		index += 1
	
	currentFloors = floors
	while True:
		floors = api.get_floor_colours()
		coloursweep.colour_sweep(strip, currentFloors, floors, 1)
		print "Waiting..."
		currentFloors = floors
		time.sleep(5)
