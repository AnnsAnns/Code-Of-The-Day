package JavaMap;

import java.util.*;

public class MapReimpl<K, V> implements java.util.Map<K, V> {
    private int _counter;
    private MapPaar<K, V>[] _keyArray;

    public MapReimpl() {
        clear();
    }

    /**
     * Put a new MapPaar into the array, doubles the size of the array if needed
     * @param mapPaar The MapPaar that should be added
     */
    private void putInto(MapPaar<K, V> mapPaar) {
        // We need to increase the array size
        if (_keyArray.length == _counter) {
            int newSize;
            if (_counter == 0) {
                newSize = 1;
            } else {
                newSize = _counter*2;
            }
            _keyArray = Arrays.copyOf(_keyArray, newSize);
        }
        _keyArray[_counter] = mapPaar;
        _counter += 1;
    }

    /**
     * Removes the MapPaar at the specific index, also moves all MapPaars one up after removing it
     * @param number The index within the array that should be removed
     */
    private void removeIndex(int number) {
        if (number == _counter-1) {
            _keyArray[number] = null;
            _counter -= 1;
        } else if (number <= _counter) {
            for (int i = number; i < _counter; i++) {
                if (_keyArray.length == i+1) {
                    _keyArray[i] = null;
                    break;
                }
                _keyArray[i] = _keyArray[i+1];
            }
            _counter -= 1;
        }
    }

    @Override
    public int size() {
        return _counter;
    }

    @Override
    public boolean isEmpty() {
        return _counter == 0;
    }

    @Override
    public boolean containsKey(Object key) {
        for (int i = 0; i < _counter; i++) {
            if (_keyArray[i] != null && _keyArray[i].getKey() == key) {
                return true;
            }
        }
        return false;
    }

    @Override
    public boolean containsValue(Object value) {
        for (int i = 0; i < _counter; i++) {
            if (_keyArray[i] != null && _keyArray[i].getValue() == value) {
                return true;
            }
        }
        return false;
    }

    @Override
    public V get(Object key) {
        for (int i = 0; i < _counter; i++) {
            if (_keyArray[i] != null && _keyArray[i].getKey() == key) {
                return _keyArray[i].getValue();
            }
        }
        return null;
    }

    @Override
    public V put(K key, V value) {
        V oldValue;

        for (int i = 0; i < _counter; i++) {
            if (_keyArray[i] != null && _keyArray[i].getKey() == key) {
                oldValue = _keyArray[i].getValue();
                _keyArray[i].setValue(value);
                return oldValue;
            }
        }
        putInto(new MapPaar<>(key, value));
        return null;
    }

    @Override
    public V remove(Object key) {
        V oldValue;
        for (int i = 0; i < _counter; i++) {
            if (_keyArray[i] != null && _keyArray[i].getKey() == key) {
                oldValue = _keyArray[i].getValue();
                removeIndex(i);
                return oldValue;
            }
        }
        return null;
    }

    @Override
    public void putAll(Map m) {
        for (Object key : m.keySet()) {
            putInto(new MapPaar<>(key, m.get(key)));
        }
    }

    @Override
    public void clear() {
        _counter = 0;
        _keyArray = new MapPaar[0];
    }

    @Override
    public Set<K> keySet() {
        Set<K> keys = new HashSet<>();

        for (int i = 0; i < _counter; i++) {
            keys.add(_keyArray[i].getKey());
        }

        return keys;
    }

    @Override
    public Collection<V> values() {
        List<V> values = new ArrayList<>();

        for (int i = 0; i < _counter; i++) {
            values.add(_keyArray[i].getValue());
        }

        return values;
    }

    @Override
    public Set<Entry<K, V>> entrySet() {
        return new HashSet<>(Arrays.asList(_keyArray).subList(0, _counter));
    }
}
