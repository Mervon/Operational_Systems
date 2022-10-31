import java.awt.desktop.SystemSleepEvent;

public class Main {
    static class solution {
        long def_1(long n) {
            long a = 0, b = 3, c = 3, N = 100000000;
            for (long i = 0; i < n; ++i) {
                for (long j = 1; j < N; ++j) {
                    a = a + b * 2 + c - i;
                }
            }
            return a;
        }
    }

    public static void main(String[] args) {
        solution sol = new solution();
        long startTime = System.nanoTime();
        sol.def_1(15);
        long endTime = System.nanoTime();
        double duration = (endTime - startTime);
        double sec_ = 1000000000;
        System.out.println(duration / sec_);

        for (int gh = 0; gh < 30; ++gh) {
            startTime = System.nanoTime();
            sol.def_1(15);
            endTime = System.nanoTime();
            duration = (endTime - startTime);
            System.out.println(duration / sec_);
        }

        //6.9568618 + 7.2284726
    }
}