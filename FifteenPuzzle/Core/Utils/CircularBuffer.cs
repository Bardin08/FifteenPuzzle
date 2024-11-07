namespace FifteenPuzzle.Core.Utils;

public class CircularBuffer<T>(int n)
{
    private int _head;
    private readonly T[] _buffer = new T[n];

    public int Count { get; private set; }

    public void Push(T value)
    {
        Count = Count++ <= _buffer.Length ? Count++ : _buffer.Length;

        _buffer[_head] = value;
        _head = (_head + 1) % _buffer.Length;
    }

    public T Pop()
    {
        if (Count is 0)
            throw new InvalidOperationException("Buffer is empty");

        Count--;
        _head = _head - 1 >= 0 ? _head - 1 : _buffer.Length - 1;

        return _buffer[_head];
    }
}