# POC - Zero Configuration Networing (ZeroConf) in .NET

This is a POC of using zero-configuration networking using .NET and the net-msdn library.  Zero-configuration is the bases of Apple's [Bonjour](https://developer.apple.com/bonjour/) technology used from everything from finding printers to airdrop.  

Bonjour, also known as zero-configuration networking, enables automatic discovery of devices and services on a local network using industry standard IP protocols. Bonjour makes it easy to discover, publish, and resolve network services with a sophisticated, yet easy-to-use, programming interface.

This technology can be used to find other mobile apps with the same "service", thus allowing you to find other mobile apps you could use to get the IP Address from to perform PTP syncing using Couchbase Lite.  This POC uses the net-mdsn nuget package and is a proof of concept of discovery working on iOS, iPadOS, MacOS, and Android.  Windows would theoricitally work, but I haven't tested it fully.

To learn more about this read this excellent blog article on mDNS Responder:
https://andriydruk.com/post/mdnsresponder/

I streamed building the app on YouTube for those that would like to see how it was built and why:
[YouTube](https://www.youtube.com/watch?v=kiSDGD8MJow)

This is provided AS IS with no support as it's a POC.
