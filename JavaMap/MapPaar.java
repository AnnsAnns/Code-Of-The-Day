package JavaMap;

import java.util.Map;

/**
 * Class MapPaar implementing Map.Entry
 * @param <K> The type of the key
 * @param <V> The type of the value
 */
public class MapPaar<K, V> implements Map.Entry<K, V> {
    final private K _key;
    private V _value;

    /**
     * Constructor for MapPaar
     * @param key The key of the pair, must be of the same type
     * @param value The value of the pair, must be of the same type
     */
    public MapPaar(Object key, Object value) {
        _key = (K) key;
        _value = (V) value;
    }

    @Override
    public K getKey() {
        return _key;
    }

    @Override
    public V getValue() {
        return _value;
    }

    @Override
    public V setValue(V value) {
        if (value.getClass() != _value.getClass()) {
            throw new IllegalArgumentException("The value must be of the same type");
        }
        V oldValue = _value;
        _value = value;
        return oldValue;
    }
}
