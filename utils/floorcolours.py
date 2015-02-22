__author__ = 'Mattias'
import urllib2
import json


def get_floor_colours():
	url = "http://api.colourbynumbers.org/cbn-live/getColours"
	request = urllib2.Request(url)
	opener = urllib2.build_opener()
	file = opener.open(request)
	data = json.load(file)
	return data["colours"]

