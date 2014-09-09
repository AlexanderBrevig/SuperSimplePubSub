SuperSimplePubSub
=================

C# SuperSimple PubSub implementation

This is simple:

    string test = " world";
    Action<object> hi = (ob) => { Console.WriteLine("hi" + ob); };
    Pub.Sub["hello"] = hi;
    Pub.Sub["hello"] = (ob) => { Console.WriteLine("hello" + ob); };
    Pub.Sub["hello"](test);

    Pub.Sub.Remove("hello", hi);
    Pub.Sub["hello"](test);
    Console.ReadKey();
    
Be sure to:

    PM> Install-Package SuperSimplePubSub
