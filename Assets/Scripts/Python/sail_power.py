import matplotlib.pyplot as plt
from numpy import *


alpha = 0 * pi / 180  # wind direction
beta = 0 * pi / 180  # ship direction
w = 1  # wind speed
a = 1  # lift arg
b = 1  # ship speed

theta = arange(beta-90, beta+90, 1) * pi / 180  # sail face direction


def f(theta, alpha, beta, w, a, b):
    v_x = w*cos(alpha)-b*cos(beta)
    v_y = w*sin(alpha)-b*sin(beta)
    lift = abs(a * (v_x * (-sin(theta) + v_y * cos(theta))))
    resistance = v_x * cos(theta) + v_y * sin(theta)
    return cos(theta - beta) * (lift + resistance)


for i in range(0, 90, 20):
    beta = i * pi / 180
    plt.plot((theta-beta)*180/pi, f(theta, alpha, beta,
             w, a, b), label="beta=%d" % (i))

plt.xlabel("theta - beta")
plt.ylabel("T")
plt.legend()
plt.show()
