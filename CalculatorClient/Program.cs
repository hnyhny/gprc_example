// Copyright 2015 gRPC authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using Grpc.Core;
using Helloworld;

namespace CalculatorClient
{
    class Program
    {
        public static void Main(string[] args)
        {
            Channel channel = new Channel("127.0.0.1:30051", ChannelCredentials.Insecure);

            var client = new Calculator.CalculatorClient(channel);
            Console.WriteLine("Ein Simpler taschenrechner, der zwei werte entgegen nimmt und dann einen Operator ausw�hlen l�sst");

            Console.WriteLine("Erste Zahl:");

            var leftValue = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Zweite Zahl");
            var rightValue = Convert.ToInt32(Console.ReadLine());

            TwoIntegerRequest request = new TwoIntegerRequest { LeftValue = leftValue, RightValue = rightValue };

            Console.WriteLine("Operation ausw�hlen: 1 f�r Addieren, 2 f�r Multiplizieren, 3 f�r Substrahieren");
            var operation = Convert.ToInt32(Console.ReadLine());
            SingleIntegerReply reply = operation switch
            {
                1 =>
                   reply = client.Add(request),
                2 =>
                   reply = client.Multiply(request),
                3 =>
                   reply = client.Subtract(request)

            };
            Console.WriteLine("Greeting: " + reply.Result);

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
