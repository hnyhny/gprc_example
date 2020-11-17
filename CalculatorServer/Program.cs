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
using System.Threading.Tasks;
using Grpc.Core;
using Helloworld;

namespace CalculatorServer
{
    class CalculatorImpl : Calculator.CalculatorBase
    {
        // Server side handler of the SayHello RPC
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
        }

        public override Task<SingleIntegerReply> Add(TwoIntegerRequest request, ServerCallContext context)
        {
            return Task.FromResult(new SingleIntegerReply { Result = request.LeftValue + request.RightValue });
        }

        public override Task<SingleIntegerReply> Multiply(TwoIntegerRequest request, ServerCallContext context)
        {
            return Task.FromResult(new SingleIntegerReply { Result = request.LeftValue * request.RightValue });
        }

        public override Task<SingleIntegerReply> Subtract(TwoIntegerRequest request, ServerCallContext context)
        {
            return Task.FromResult(new SingleIntegerReply { Result = request.LeftValue - request.RightValue });
        }
    }

    class Program
    {
        const int Port = 30051;

        public static void Main(string[] args)
        {
            Server server = new Server
            {
                Services = { Calculator.BindService(new CalculatorImpl()) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("Calculator server listening on port " + Port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
