#!/usr/bin/env python

from functools import reduce
import sys


def description():
    print('Convert spoken constiuents of numbers (positive integers) into the intended integers')


def get_last_index(seq, predicate):
    for i in range(len(seq)):
        if predicate(seq[-1 - i]):
            return len(seq) - 1 - i
    return -1


def process_next_spoken_number(state, next_num):
    print('>>> state={0:s}, num={1:d}'.format(str(state), next_num))
    if state == [] or next_num < state[-1]:
        state.append(next_num)
        # print('At position #{0:d}: Appended ({1:d}) to get {2!s}'.format(i, next_num, state))
    elif next_num > state[-1]:
        # Take the numbers since the last one larger than val. 
        # Replace them with the product of their sum times the current num.
        larger_val_index = get_last_index(state, lambda x: x > next_num)
        new_val = next_num * sum(state[larger_val_index + 1:])
        state = state[0 : larger_val_index + 1] + [new_val]
        # print('At position #{0:d}: Multiplied by units ({1:d}) to get {2!s}'.format(i, next_num, state))
    else:
        # print('Error at number #{0:d} ({1:d})'.format(i, next_num))
        return -1
    return state


def process_spoken_numbers(spoken_nums):
    reduced = reduce(process_next_spoken_number, spoken_nums, [])
    return sum(reduced)
    # state = []
    # for num in spoken_nums:
    #     state = process_next_spoken_number(state, num)
    # return sum(state)


def test_process_spoken_numbers(spoken_nums):
    print('Input numbers={}'.format(spoken_nums))
    result = process_spoken_numbers(spoken_nums)
    print('Result={0:,d}'.format(result))
    print()



if __name__ == '__main__':
    description()
    test_process_spoken_numbers([0])
    test_process_spoken_numbers([4, 100, 90, 2])
    test_process_spoken_numbers([3, 100, 50, 7, 1000, 4, 100, 90, 2])
    test_process_spoken_numbers([8, 100, 16, 1000000, 3, 100, 50, 7, 1000, 4, 100, 90, 2])

