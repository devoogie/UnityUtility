using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T>:IEnumerator,IEnumerable where T : IComparable<T>
{
    public int Count { get { return _heap.Count; } }

    List<T> _heap = new List<T>();

    // O(logN)
    public void Push(T data)
    {
        // 힙의 맨 끝에 새로운 데이터를 삽입한다
        _heap.Add(data);

        int now = _heap.Count - 1;
        // 도장깨기를 시작
        while (now > 0)
        {
            // 도장깨기를 시도
            int next = (now - 1) / 2;
            if (_heap[now].CompareTo(_heap[next]) < 0)
                break; // 실패

            // 두 값을 교체한다
            T temp = _heap[now];
            _heap[now] = _heap[next];
            _heap[next] = temp;

            // 검사 위치를 이동한다
            now = next;
        }
    }
    public T Peek()
    {  
        return _heap[0];
    }   
    // O(logN)
    public T Pop()
    {
        // 반환할 데이터를 따로 저장
        T ret = _heap[0];

        // 마지막 데이터를 루트로 이동한다
        int lastIndex = _heap.Count - 1;
        _heap[0] = _heap[lastIndex];
        _heap.RemoveAt(lastIndex);
        lastIndex--;

        // 역으로 내려가는 도장깨기 시작
        int now = 0;
        while (true)
        {
            int left = 2 * now + 1;
            int right = 2 * now + 2;

            int next = now;
            // 왼쪽값이 현재값보다 크면, 왼쪽으로 이동
            if (left <= lastIndex && _heap[next].CompareTo(_heap[left]) < 0)
                next = left;
            // 오른값이 현재값(왼쪽 이동 포함)보다 크면, 오른쪽으로 이동
            if (right <= lastIndex && _heap[next].CompareTo(_heap[right]) < 0)
                next = right;

            // 왼쪽/오른쪽 모두 현재값보다 작으면 종료
            if (next == now)
                break;

            // 두 값을 교체한다
            T temp = _heap[now];
            _heap[now] = _heap[next];
            _heap[next] = temp;
            // 검사 위치를 이동한다
            now = next;
        }

        return ret;
    }

    int position;
    public object Current => _heap[position];


    public bool MoveNext()
    {
        if (position == _heap.Count - 1)
        {
            Reset();
            return false;
        }

        position++;
        return (position < _heap.Count);
    }

    public void Reset()
    {
        position = -1;
    }

    // IEnumerable 멤버
    public IEnumerator GetEnumerator()
    {
        for (int i = 0; i < _heap.Count; i++)
        {
            //  yield: 데이터를 하나씩 리턴할 때 사용
            yield return (_heap[i]);
        }
        }
}
