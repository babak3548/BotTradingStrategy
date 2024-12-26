// See https://aka.ms/new-console-template for more information
using PreprocessDataStocks.CandleConsumerModule;
using PreprocessDataStocks.CandleProducerModule;
using PreprocessDataStocks.CandleQueueManagerModule;
using PreprocessDataStocks.Models;
using PreprocessDataStocks.PartnerApi;
using PreprocessDataStocks.TickQueueManagerModule;
using PreprocessDataStocks;
using PreprocessDataStocks.CandleConsumerModule.TickConsumerModule;
using PreprocessDataStocks.CandleProducerModule.TickProducerModule;

PreprocessDataStockContext dbContextCandleConsumer = new PreprocessDataStockContext();
PreprocessDataStockContext dbContextTickProducer = new PreprocessDataStockContext();
PreprocessDataStockContext dbContextTickConsumer = new PreprocessDataStockContext();
ConfigBot.LoadConfiguration(dbContextCandleConsumer);

Console.WriteLine("Start  process Data Stock");
//CandleTickDataReaderCsv readerCsv = CandleTickDataReaderCsv.Instance;
//TwelveDataApiReader readerCsv =  TwelveDataApiReader.Instance;
CandleQueueManager candleQueueManager = new CandleQueueManager();
CandleProducer candleProducer = new CandleProducer(candleQueueManager);
CandleConsumer candleConsumer = new CandleConsumer(candleQueueManager, dbContextCandleConsumer);

// Create and start the producer thread
Thread candleProducerThread = new Thread(candleProducer.Produce);
candleProducerThread.Start();

// Create and start the consumer thread
Thread candleConsumerThread = new Thread(candleConsumer.Consume);
candleConsumerThread.Start();

/////////

TickQueueManager tickQueueManager = new TickQueueManager();
TickProducer tickProducer = new TickProducer(tickQueueManager, dbContextTickProducer);
TickConsumer tickConsumer = new TickConsumer(tickQueueManager, dbContextTickConsumer);

// Create and start the producer thread
Thread TickproducerThread = new Thread(tickProducer.Produce);
TickproducerThread.Start();

// Create and start the consumer thread
Thread tickConsumerThread = new Thread(tickConsumer.Consume);
tickConsumerThread.Start();

// Wait for both threads to complete
Console.WriteLine($"Wait for  threads to complete");
candleProducerThread.Join();
candleConsumerThread.Join();
TickproducerThread.Join();
tickConsumerThread.Join();
Console.WriteLine($"compleate  Main theread");

Console.WriteLine("End process Data Stock");
