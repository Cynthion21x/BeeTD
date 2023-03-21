from os import listdir
from os.path import isfile, isdir, join
import os

def TopLineCount(path):

    if isdir(path):
        return DirLineC(path)
    elif isfile(path):
        return len(open(path, 'rb').readlines())
    else:
        return 0

def DirLineC(dir):
    return sum(map(lambda item: TopLineCount(join(dir, item)), listdir(dir)))

print(TopLineCount(os.getcwd()))