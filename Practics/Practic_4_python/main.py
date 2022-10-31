import time

def def_1(n):
    a = 0
    b = 3
    c = 3
    N = 100000000
    for i in range(n):
        for j in range(1, N):
            a = a + b * 2 + c - i
    return a

if __name__ == '__main__':
    t0 = time.time()
    print(def_1(15))
    print("Time elapsed in seconds: ", time.time() - t0)
    #92 sec
