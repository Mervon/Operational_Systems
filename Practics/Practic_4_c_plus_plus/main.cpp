#include <iostream>
#include <time.h>

using namespace std;

long long int def_1(long long int n) {
    long long int  a = 0, b = 3, c = 3, N = 100000000;
    for (long long int i = 0; i < n; ++i) {
        for (long long int j = 1; j < N; ++j) {
            a = a + b * 2 + c - i;
        }
    }
    return a;
}

int main() {
    double average = 0;
    for (int i = 0; i < 30; ++i) {
        clock_t tStart = clock();
        def_1(15);
        average += (double)(clock() - tStart)/CLOCKS_PER_SEC;
    }
    cout << average/30 << endl; //3.0293 + 3.02443 + 3.02373

}
