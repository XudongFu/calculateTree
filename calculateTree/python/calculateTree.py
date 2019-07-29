import clr
import sys
# 导入clr时这个模块最好也一起导入，这样可用使用AddReference()方法
import System

# input()

from System import Array
from System import String

# 打印当前.net运行时的版本
sys.path.append("F:\code\calculateTree")
clr.FindAssembly("calculateTree.exe")
from calculateTree.free import *
c=CalculateEngine()
print(c.Parse("a+b=5"))
print(c.Parse("b=7"))



