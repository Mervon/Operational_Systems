import java.util.Scanner;
import java.util.Stack;
import java.util.concurrent.ThreadLocalRandom;

class Consumer extends Thread{
    public boolean is_interupted = false;
    public void Stop() {
        is_interupted = true;
    }
    MyStack st_;
    Consumer(MyStack st){
        this.st_=st;
    }
    public void run() {
        try {
            while (!is_interupted) {
                synchronized (st_) {
                    if (st_.st.size() != 0) st_.st.pop();
                }
                Thread.sleep(50);
            }
        } catch(InterruptedException e){
            System.out.println(getName() + " has been interrupted");
        }
    }
}

class Controller extends Thread{


    public void Stop() {
        interrupt();
    }
    MyStack st_;
    Producer p1_, p2_, p3_;
    Consumer c1_, c2_;

    MyFlag mf_;
    Controller(MyStack st, Producer p1, Producer p2, Producer p3, Consumer c1, Consumer c2, MyFlag mf){
        this.st_ = st;
        this.p1_ = p1;
        this.p2_ = p2;
        this.p3_ = p3;
        this.c1_ = c1;
        this.c2_ = c2;
        this.mf_ = mf;
    }
    public void run(){
        while(mf_.check.size() == 1) {

            synchronized (st_.st) {
                //System.out.println(st_.st.size());
                int curr_size = st_.st.size();
                if (curr_size > 100) {
                    if(!p1_.is_interupted) p1_.Stop();
                    if(!p2_.is_interupted) p2_.Stop();
                    if(!p3_.is_interupted) p3_.Stop();
                } else if (curr_size <= 80 && curr_size > 0) {
                    if(p1_.is_interupted) {
                        p1_ = new Producer(st_);
                        p1_.start();
                    }
                    if(p2_.is_interupted) {
                        p2_ = new Producer(st_);
                        p2_.start();
                    }
                    if(p3_.is_interupted) {
                        p3_ = new Producer(st_);
                        p3_.start();
                    }
                } else if (curr_size == 0) {
                    if(!c1_.is_interupted) c1_.Stop();
                    if(!c2_.is_interupted) c2_.Stop();
                }
            }

            try {
                Thread.sleep(32);
            } catch (InterruptedException e) {

            }
        }

        if (!p1_.is_interupted) p1_.Stop();
        if (!p2_.is_interupted) p2_.Stop();
        if (!p3_.is_interupted) p3_.Stop();

        while(true) {
            //System.out.println(st_.st.size());
            synchronized (st_) {
                if (st_.st.size() == 0) {
                    c1_.Stop();
                    c2_.Stop();
                    break;
                }
            }

            try {
                Thread.sleep(32);
            } catch (InterruptedException e) {

            }
        }
    }
}



class Producer extends Thread{
    boolean is_interupted = false;
    public void Stop() {
        is_interupted = true;
    }
    MyStack st_;
    Producer(MyStack st){
        this.st_=st;
    }
    public void run(){
        try {
            while (!is_interupted) {
                int randomNum = ThreadLocalRandom.current().nextInt(1, 100 + 1);
                CommonResource res = new CommonResource();
                res.x = randomNum;
                synchronized (st_) {
                    st_.st.push(res);
                }
                Thread.sleep(50);
            }
        } catch(InterruptedException e){
            System.out.println(getName() + " has been interrupted");
        }
    }
}

class CommonResource {
    int x = 0;
}

class MyStack {
    Stack<CommonResource> st;
}

class MyFlag {
    Stack<CommonResource> check;
}

public class Main {
    public static void main(String[] args) {
        MyStack pool = new MyStack();
        Stack<CommonResource> st_ = new Stack<>();
        for (int i = 0; i < 200; i++) {
            int next = ThreadLocalRandom.current().nextInt(1, 100 + 1);
            CommonResource cr = new CommonResource();
            cr.x = next;
            st_.push(cr);
        }
        pool.st = st_;
        System.out.println("Размер очереди в начале выполнения программы: " + st_.size());
        Producer t1 = new Producer(pool);
        t1.setName("Thread 1");
        t1.start();
        Producer t2 = new Producer(pool);
        t2.setName("Thread 2");
        t2.start();
        Producer t3 = new Producer(pool);
        t3.setName("Thread 2");
        t3.start();
        Consumer c1 = new Consumer(pool);
        c1.setName("Thread 3");
        c1.start();
        Consumer c2 = new Consumer(pool);
        c2.setName("Thread 3");
        c2.start();

        MyFlag mf = new MyFlag();
        mf.check = new Stack<CommonResource>();
        mf.check.push(new CommonResource());
        Controller controller = new Controller(pool, t1, t2, t3, c1, c2, mf);
        controller.setName("Thread 4");
        controller.start();

        Scanner in = new Scanner(System.in);
        System.out.print("Введите 0 для остановки программы: \n");
        String str = in.nextLine();
        while(!str.equals("0")) {
            System.out.print("Введите 0 для остановки программы: \n");
            str = in.nextLine();
        }
        mf.check.push(new CommonResource());
        in.close();
        try {
            t1.join();
            t2.join();
            t3.join();
            c1.join();
            c2.join();
        } catch (InterruptedException e) {
            System.out.println("Something went wrong opsieeeeeeeeeeee");
        }

        System.out.println("Размер очереди в конце выполнения программы: " + st_.size());
    }
}