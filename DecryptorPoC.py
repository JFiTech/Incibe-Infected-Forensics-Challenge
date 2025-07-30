import base64
from Crypto.Cipher import AES
import hashlib

def decode_base64_bytes(coded_string): 
    decoded_bytes = base64.b64decode(coded_string)
    return decoded_bytes

def get_key_variants(key_string):
    """Generate different possible key derivations"""
    variants = {}
    
    # Method 1: Direct UTF-8 encoding (what we tried before)
    variants["utf8"] = key_string.encode('utf-8')
    
    # Method 2: ASCII encoding
    try:
        variants["ascii"] = key_string.encode('ascii')
    except:
        pass
    
    # Method 3: MD5 hash (common in older implementations)
    variants["md5"] = hashlib.md5(key_string.encode('utf-8')).digest()
    
    # Method 4: SHA1 truncated to 16 bytes
    variants["sha1_16"] = hashlib.sha1(key_string.encode('utf-8')).digest()[:16]
    
    # Method 5: SHA256 truncated to 16 bytes
    variants["sha256_16"] = hashlib.sha256(key_string.encode('utf-8')).digest()[:16]
    
    # Method 6: Direct bytes (if the string represents hex)
    try:
        if len(key_string) == 32:  # 16 bytes in hex
            variants["hex"] = bytes.fromhex(key_string)
    except:
        pass
    
    # Method 7: Repeat the string to fill 16 bytes if it's shorter
    if len(key_string.encode('utf-8')) < 16:
        repeated = (key_string * ((16 // len(key_string)) + 1))[:16]
        variants["repeated"] = repeated.encode('utf-8')
    
    return variants

def decrypt_with_key(key_bytes, encrypted_bytes, key_method):
    try:
        print(f"\n--- Trying key method: {key_method} ---")
        print(f"Key bytes: {key_bytes.hex()}")
        print(f"Key length: {len(key_bytes)}")
        
        if len(key_bytes) != 16:
            print(f"Invalid key length: {len(key_bytes)} (expected 16)")
            return None
        
        # Try CBC with key as IV (matching the vulnerable C# code)
        cipher = AES.new(key_bytes, AES.MODE_CBC, key_bytes)
        decrypted = cipher.decrypt(encrypted_bytes)
        print(f"Decrypted (hex): {decrypted.hex()}")
        
        # Try different padding removal strategies
        for pad_len in range(1, 17):
            try:
                trimmed = decrypted[:-pad_len]
                text = trimmed.decode('utf-8')
                if text.isprintable() and len(text.strip()) > 0:
                    print(f"SUCCESS with {key_method}, padding {pad_len}: '{text}'")
                    return text
            except:
                continue
        
        # Try without padding removal
        try:
            text = decrypted.decode('utf-8', errors='ignore').rstrip('\x00\x01\x02\x03\x04\x05\x06\x07\x08\x09\x0a\x0b\x0c\x0d\x0e\x0f\x10')
            if text.isprintable() and len(text.strip()) > 0:
                print(f"SUCCESS with {key_method}, no padding removal: '{text}'")
                return text
        except:
            pass
            
    except Exception as e:
        print(f"Error with {key_method}: {e}")
    
    return None

def decrypt(key_string, encrypted_bytes):
    print(f"Original key string: '{key_string}'")
    print(f"Encrypted bytes length: {len(encrypted_bytes)}")
    
    # Get all possible key variants
    key_variants = get_key_variants(key_string)
    
    # Try each key variant
    for method, key_bytes in key_variants.items():
        result = decrypt_with_key(key_bytes, encrypted_bytes, method)
        if result:
            return result
    
    print("\nNo successful decryption found with any key method")
    return None

def main():
    key_string = "KHx4V2LSBsFKtAk8"
    coded_string = "o0ylbT4B146ILznhUFqBBkv9Cq+8VtR9Svr4Ux7Cnsg="
    
    print(f"Original base64: {coded_string}")
    
    # Decode base64 to get encrypted bytes
    encrypted_bytes = decode_base64_bytes(coded_string)
    print(f"Decoded bytes: {encrypted_bytes.hex()}")
    
    # Try decryption with various key derivation methods
    result = decrypt(key_string, encrypted_bytes)
    
    if result:
        print(f"\nFINAL RESULT: '{result}'")
    else:
        print("\nDecryption failed with all methods")

if __name__ == "__main__":
    main()