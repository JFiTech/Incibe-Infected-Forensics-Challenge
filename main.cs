using System;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

[StructLayout(LayoutKind.Auto)]
[CompilerGenerated]
private struct <Main>d__1 : IAsyncStateMachine
{
    public int <>1__state;

    public AsyncTaskMethodBuilder <>t__builder;

    public string[] args;

    private TaskAwaiter<HttpResponseMessage> <>u__1;

    private void MoveNext()
    {
        //IL_005e: Unknown result type (might be due to invalid IL or missing references)
        int num = <>1__state;
        try
        {
            TaskAwaiter<HttpResponseMessage> awaiter;
            if (num != 0)
            {   
                // outputs the number of command-line arguments passed to the program when it was executed.
                Console.WriteLine(args.Length);

                // Retrieves the first command-line argument passed to a program.
                string s = args[0];
                
                /* The AES key is generated from the string "KHx4V2LSBsFKtAk8" 
                 * which is 16 characters long.
                 * Since each character in this string represents one byte (8 bits), and the string 
                 * is 16 characters long, this creates a 128-bit AES key (16 bytes × 8 bits/byte = 128 bits).
                 * AES supports three key sizes:
                 * 128-bit (16 bytes) - which is what's being used here
                 * 192-bit (24 bytes)
                 * 256-bit (32 bytes)
                 * The 128-bit key size is the most commonly used and provides strong security for 
                 * most applications, though 256-bit keys are sometimes preferred for highly 
                 * sensitive data or when longer-term security is required.
                 */
                byte[] key = GetKey("KHx4V2LSBsFKtAk8"); // ENCRYPTION KEY
                
                /******
                /* VULNERABLE CODE:
                /* Using the same value for both the key and IV is a serious security flaw. 
                /* The IV should be unique for each encryption operation and never reused with the same key. 
                /* When you use the key as the IV, you're essentially using a predictable, static IV, which makes
                /* your encryption vulnerable to attacks and can reveal patterns in your encrypted data.
                *****/
                ICryptoTransform cryptoTransform = Aes.Create().CreateEncryptor(key, key);   

                /*******
                 * Converts a string into its byte array representation using UTF-8 encoding,  
                 * which is a necessary step before performing cryptographic operations.
                ********/
                byte[] bytes = Encoding.UTF8.GetBytes(s);

                /********
                 * Encrypted data is raw binary that may contain unprintable characters
                *********/
                string text = Convert.ToBase64String(cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length));
                
                // outputs the value of the text variable to the console, followed by a line break.
                Console.WriteLine(text);
                
                // CREATE A http SERVER
                HttpClient val = new HttpClient();
                
                /*
                 *  First, args[1] retrieves the second command-line argument (remember that arrays are 
                 *  zero-indexed, so args[0] is the first argument and args[1] is the second). This likely 
                 *  contains a base URL or endpoint address. Then it appends the literal string "/?data=", 
                 *  which creates a query parameter structure. Finally, it concatenates the value of the text 
                 *  variable, which based on the surrounding code context appears to contain Base64-encoded 
                 *  encrypted data.
                 *   
                 *  The resulting text2 variable will contain a complete URL that might look something 
                 *  like "https://example.com/?data=SGVsbG8gV29ybGQ=" if args[1] was "https://example.com" 
                 *  and text contained "SGVsbG8gV29ybGQ=". This URL format is commonly used for GET requests 
                 *  where data needs to be passed as query parameters.
                 */
                string text2 = args[1] + "/?data=" + text;
                

                awaiter = val.GetAsync(text2).GetAwaiter();
                
                if (!awaiter.IsCompleted)
                {
                    num = (<>1__state = 0);
                    <>u__1 = awaiter;
                    <>t__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
                    return;
                }
            }
            else
            {
                awaiter = <>u__1;
                <>u__1 = default(TaskAwaiter<HttpResponseMessage>);
                num = (<>1__state = -1);
            }
            Console.WriteLine(awaiter.GetResult().StatusCode);
        }
        catch (Exception exception)
        {
            <>1__state = -2;
            <>t__builder.SetException(exception);
            return;
        }
        <>1__state = -2;
        <>t__builder.SetResult();
    }

    void IAsyncStateMachine.MoveNext()
    {
        //ILSpy generated this explicit interface implementation from .override directive in MoveNext
        this.MoveNext();
    }

    [DebuggerHidden]
    private void SetStateMachine(IAsyncStateMachine stateMachine)
    {
        <>t__builder.SetStateMachine(stateMachine);
    }

    void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
    {
        //ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
        this.SetStateMachine(stateMachine);
    }
}
