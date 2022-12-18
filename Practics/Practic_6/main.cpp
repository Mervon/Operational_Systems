// Вариант 2, "Распределение памяти перемещаемыми разделами"

#include <iostream>
#include <vector>
#include <bitset>
#include <cstddef>

using namespace std;

std::ostream& operator<< (std::ostream& os, std::byte b) {
    return os << std::bitset<8>(std::to_integer<int>(b));
}

const int GLOBAL_MEMORY_SIZE = 65556;

void ClearBytesInRange(std::byte* bytes, int clear_from, int clear_to) {
    for (int i = clear_from; i < clear_to; i++) {
        bytes[i] &= std::byte{0b0};
    }
}

void CheckForBytesInRange(std::byte* bytes, int check_from, int check_to) {
    for (int i = check_from; i < check_to; i++) {
        bool check = std::bitset<8>(std::to_integer<int>(bytes[i])) == std::bitset<8>(0);
        if (!check) {
            cout << "Check failed on byte {" << i << "}" << endl << "The byte value is {" << std::bitset<8>(std::to_integer<int>(bytes[i])) << "}" << endl;
            return;
        }
    }

    cout << "Check completed, all bytes in range is 0s" << endl;
}

class Task{
public:
    Task(int size) : size_(size) {
        data_ = new byte[size_];
        ClearBytesInRange(data_, 0, size_);
    }

    void FillBytesInRange(int fill_from, int fill_to) {
        if (fill_from > fill_to) {
            cout << "Wrong input" << endl;
            return;
        }

        if (fill_to > size_) {
            cout << "Wrong input" << endl;
            return;
        }

        for (int i = fill_from; i < fill_to; ++i) {
            data_[i] |= byte{0b11111111};
        }
    }

    byte* GetData() {
        return data_;
    }

    int GetSize() const {
        return size_;
    }

    ~Task() {
        delete[] data_;
    }
private:
    int size_;
    byte* data_;
};

class MemoryTable {
public:
    void LoadTask(Task& task) {
        if (task.GetSize() > GLOBAL_MEMORY_SIZE) {
            cout << "The task is too big" << endl;
            return;
        }
        int start_in_task = 0, task_size = task.GetSize();
        bool was_unloaded_at_least_once = false;
        while(!CheckForEnoughMemory(task_size)) {
            UnloadTask();
            was_unloaded_at_least_once = true;
        }
        if (was_unloaded_at_least_once) {
            RestructRawMemory();
        }
        if (number_to_task.empty()) {
            number_to_task.emplace_back(0,task.GetSize());
        } else {
            number_to_task.emplace_back(
                    number_to_task[number_to_task.size() - 1].first
                    + number_to_task[number_to_task.size() - 1].second,
                    task.GetSize()
            );
        }

        int start_in_memory = number_to_task[number_to_task.size() - 1].first;

        for (; start_in_task < task_size; ++start_in_memory, ++start_in_task) {
            raw_memory[start_in_memory] = task.GetData()[start_in_task];
        }
    }

    void UnloadTask() {
        vector<pair<int, int>> temp;
        for (int i = 1; i < number_to_task.size(); ++i) {
            temp.push_back(number_to_task[i]);
        }
        number_to_task = temp;
    }

    byte* GetRawMemory() {
        return raw_memory;
    }
private:
    void RestructRawMemory() {
        vector<pair<int, int>> temp;
        int start_in_memory = 0;
        for (auto& item : number_to_task) {
            int start_in_task = item.first, end_in_task = item.second;
            temp.emplace_back(start_in_memory, end_in_task);
            for (int i = 0; i < end_in_task; ++start_in_task, ++start_in_memory, ++i) {
                raw_memory[start_in_memory] = raw_memory[start_in_task];
            }
        }

        for (; start_in_memory < GLOBAL_MEMORY_SIZE; ++start_in_memory) {
            raw_memory[start_in_memory] = byte{0b0};
        }

        number_to_task = temp;
    }

    bool CheckForEnoughMemory(int task_size) {
        int free_amount_of_bytes = GLOBAL_MEMORY_SIZE;
        for (auto& item : number_to_task) {
            free_amount_of_bytes -= item.second;
        }

        if (task_size > free_amount_of_bytes) {
            return false;
        }

        return true;
    }

    vector<pair<int, int>> number_to_task;
    byte raw_memory[GLOBAL_MEMORY_SIZE];
};

void Test_1() {
    cout << "TEST_1_STARTED" << endl;

    byte bytes[GLOBAL_MEMORY_SIZE];

    int check_from = 0, check_to = GLOBAL_MEMORY_SIZE;

    int clear_from = 0, clear_to = GLOBAL_MEMORY_SIZE;

    ClearBytesInRange(bytes, clear_from, clear_to);

    bytes[1000] = byte{0b01010101};

    CheckForBytesInRange(bytes, check_from, check_to);

    ClearBytesInRange(bytes, 1000, 1001);

    CheckForBytesInRange(bytes, check_from, check_to);

    cout << "TEST_1_IS_DONE" << endl;
}

void Test_2() {
    cout << "TEST_2_STARTED" << endl;

    Task t{150};

    CheckForBytesInRange(t.GetData(), 0, t.GetSize());

    t.FillBytesInRange(50, t.GetSize());

    CheckForBytesInRange(t.GetData(), 0, t.GetSize());

    CheckForBytesInRange(t.GetData(), 149, t.GetSize());

    cout << "TEST_2_IS_DONE" << endl;
}

void Test_3() {
    cout << "TEST_3_STARTED" << endl;
    MemoryTable mt;
    Task t1(100);
    t1.FillBytesInRange(0, 100);
    t1.GetData()[37] = byte{0b01010101};
    t1.GetData()[0] = byte{0b11000011};
    t1.GetData()[99] = byte{0b00111100};
    Task t2(150);
    t2.FillBytesInRange(0, 150);
    t2.GetData()[123] = byte{0b10101010};

    mt.LoadTask(t1);
    mt.LoadTask(t1);
    mt.LoadTask(t1);
    mt.LoadTask(t2);
    CheckForBytesInRange(mt.GetRawMemory(), 37, GLOBAL_MEMORY_SIZE);
    CheckForBytesInRange(mt.GetRawMemory(), 137, GLOBAL_MEMORY_SIZE);
    CheckForBytesInRange(mt.GetRawMemory(), 237, GLOBAL_MEMORY_SIZE);
    CheckForBytesInRange(mt.GetRawMemory(), 423, GLOBAL_MEMORY_SIZE);
    cout << "TEST_3_IS_DONE" << endl;
}

void Test_4() {
    cout << "TEST_4_STARTED" << endl;
    MemoryTable mt;
    Task t1(20000);
    t1.FillBytesInRange(0, 100);
    t1.GetData()[9650] = byte{0b01010101};
    Task t2(30000);
    t2.FillBytesInRange(0, 150);
    t2.GetData()[23400] = byte{0b10101010};

    mt.LoadTask(t1);
    mt.LoadTask(t1);
    mt.LoadTask(t1);
    mt.LoadTask(t2);
    CheckForBytesInRange(mt.GetRawMemory(), 9650, GLOBAL_MEMORY_SIZE);
    CheckForBytesInRange(mt.GetRawMemory(), 43400, GLOBAL_MEMORY_SIZE);
    cout << "TEST_4_IS_DONE" << endl;
}

void Run_All_Tests() {
    Test_1();
    Test_2();
    Test_3();
    Test_4();
}

int main() {

    Run_All_Tests();

    return 0;
}
