namespace FxProTest.Lib;

public class CircularQueue<T>
{
    private readonly T[] _storage;
    // read point can be [0..n-1]
    private int _read;
    // write point can be [0..2n -1]
    // so that e can handle whole buffer (n-1 - 2n-1 == n)
    private int _write;

    public CircularQueue(int capacity)
    {
        _storage = new T[capacity];
        _read = 0;
        _write = 0;
    }

    public void Enqueue(T item)
    {
        _storage[_write % _storage.Length] = item;
        IncrementWritePointer();
    }

    private void IncrementWritePointer()
    {
        // move write to next slot
        _write++;
        // check if we make a cycle on write and reach to read pointer (buffer full)
        if (_write > _read + _storage.Length)
        {
            // move read pointer further most old slot
            _read++;
        }

        if (_read == _storage.Length)
        {
            _read = 0;
        }

        // when write pointer have made 
        if (_write == 2 * _storage.Length)
        {
            _write = _storage.Length;
        }
    }

    private void IncrementReadPointer()
    {
        _read++;
        if (_read == _storage.Length)
        {
            _read = 0;
            _write -= _storage.Length;
        }
    }

    public T Dequeue()
    {
        if(Count == 0) throw new InvalidOperationException();
        var res = _storage[_read % _storage.Length];
        IncrementReadPointer();
        
        return res;
    }
    
    public int Count => _write - _read;
}