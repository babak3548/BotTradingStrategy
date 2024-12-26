-- Turn off FOREIGN KEY constraints
ALTER TABLE [PreprocessDataStock_1].[dbo].[Order] NOCHECK CONSTRAINT ALL;

-- Allow inserting values into the identity column
SET IDENTITY_INSERT [PreprocessDataStock_1].[dbo].[Order] ON;

  -- Insert data from SourceDatabase to DestinationDatabase
INSERT INTO [PreprocessDataStock_1].[dbo].[Order] (
   [Id],
      [VolumeProfilerId]
      ,[OpenTickPrice]
      ,[OpenTickDateTime]
      ,[OrderDatetime]
      ,[LastBarsRepetion]
      ,[CounterCandelDiff]
      ,[OrderType]
      ,[TakeProfit]
      ,[StopLoss]
      ,[VPlow_high]
      ,[CloseTickPrice]
      ,[LotSize]
      ,[ReasonCloseOrders]
      ,[History]
      ,[Status]
      ,[PeriodTickMin]
      ,[CloseTickDatetime]
      ,[LastTotalBalance]
      ,[SymbolId]
      ,[Revenue]
      ,[AccountMargin]
      ,[OrderMargin]
      ,[ExClosePosAccountBalance]
      ,[ExClosePosCloseoutAsk]
      ,[ExClosePosCloseoutBid]
      ,[ExClosePosCommission]
      ,[ExClosePosFinancing]
      ,[ExClosePosFullResponseBody]
      ,[ExClosePosFullVWAP]
      ,[ExClosePosGainQuoteHomeConversionFactor]
      ,[ExClosePosHalfSpreadCost]
      ,[ExClosePosLossQuoteHomeConversionFactor]
      ,[ExClosePosMarketStatus]
      ,[ExClosePosPL]
      ,[ExClosePosPriceTran]
      ,[ExClosePosReason]
      ,[ExClosePosRelatedTransactionIDs]
      ,[ExClosePosRequestedUnits]
      ,[ExClosePosSymbol]
      ,[ExClosePosTime]
      ,[ExClosePosTradeIds]
      ,[ExClosePosUnits]
      ,[ExCreateRespAccountBalance]
      ,[ExCreateRespCloseoutAsk]
      ,[ExCreateRespCloseoutBid]
      ,[ExCreateRespCommission]
      ,[ExCreateRespFinancing]
      ,[ExCreateRespFullResponseBody]
      ,[ExCreateRespGainQuoteHomeConversionFactor]
      ,[ExCreateRespHalfSpreadCost]
      ,[ExCreateRespInitialMarginRequired]
      ,[ExCreateRespLossQuoteHomeConversionFactor]
      ,[ExCreateRespMarketStatus]
      ,[ExCreateRespPL]
      ,[ExCreateRespPriceTran]
      ,[ExCreateRespReason]
      ,[ExCreateRespRelatedTransactionIDs]
      ,[ExCreateRespTime]
      ,[ExCreateRespTradeIds]
      ,[ExCreateRespUnits]
)
SELECT 
   [Id],
      [VolumeProfilerId]
      ,[OpenTickPrice]
      ,[OpenTickDateTime]
      ,[OrderDatetime]
      ,[LastBarsRepetion]
      ,[CounterCandelDiff]
      ,[OrderType]
      ,[TakeProfit]
      ,[StopLoss]
      ,[VPlow_high]
      ,[CloseTickPrice]
      ,[LotSize]
      ,[ReasonCloseOrders]
      ,[History]
      ,[Status]
      ,[PeriodTickMin]
      ,[CloseTickDatetime]
      ,[LastTotalBalance]
      ,[SymbolId]
      ,[Revenue]
      ,[AccountMargin]
      ,[OrderMargin]
      ,[ExClosePosAccountBalance]
      ,[ExClosePosCloseoutAsk]
      ,[ExClosePosCloseoutBid]
      ,[ExClosePosCommission]
      ,[ExClosePosFinancing]
      ,[ExClosePosFullResponseBody]
      ,[ExClosePosFullVWAP]
      ,[ExClosePosGainQuoteHomeConversionFactor]
      ,[ExClosePosHalfSpreadCost]
      ,[ExClosePosLossQuoteHomeConversionFactor]
      ,[ExClosePosMarketStatus]
      ,[ExClosePosPL]
      ,[ExClosePosPriceTran]
      ,[ExClosePosReason]
      ,[ExClosePosRelatedTransactionIDs]
      ,[ExClosePosRequestedUnits]
      ,[ExClosePosSymbol]
      ,[ExClosePosTime]
      ,[ExClosePosTradeIds]
      ,[ExClosePosUnits]
      ,[ExCreateRespAccountBalance]
      ,[ExCreateRespCloseoutAsk]
      ,[ExCreateRespCloseoutBid]
      ,[ExCreateRespCommission]
      ,[ExCreateRespFinancing]
      ,[ExCreateRespFullResponseBody]
      ,[ExCreateRespGainQuoteHomeConversionFactor]
      ,[ExCreateRespHalfSpreadCost]
      ,[ExCreateRespInitialMarginRequired]
      ,[ExCreateRespLossQuoteHomeConversionFactor]
      ,[ExCreateRespMarketStatus]
      ,[ExCreateRespPL]
      ,[ExCreateRespPriceTran]
      ,[ExCreateRespReason]
      ,[ExCreateRespRelatedTransactionIDs]
      ,[ExCreateRespTime]
      ,[ExCreateRespTradeIds]
      ,[ExCreateRespUnits]
FROM [PreprocessDataStock].[dbo].[Order]
-- Turn off IDENTITY_INSERT
SET IDENTITY_INSERT [PreprocessDataStock_1].[dbo].[Order] OFF;

-- Re-enable FOREIGN KEY constraints
ALTER TABLE [PreprocessDataStock_1].[dbo].[Order] WITH CHECK CHECK CONSTRAINT ALL;

--------------------------------------------------****************************************************************************
-- Turn off FOREIGN KEY constraints
ALTER TABLE [PreprocessDataStock_1].[dbo].[VolumeProfiler] NOCHECK CONSTRAINT ALL;

-- Allow inserting values into the identity column
SET IDENTITY_INSERT [PreprocessDataStock_1].[dbo].[VolumeProfiler] ON;

  -- Insert data from SourceDatabase to DestinationDatabase
INSERT INTO [PreprocessDataStock_1].[dbo].[VolumeProfiler] (
       [Id],
       [Datetime]
      ,[High]
      ,[Low]
      ,[LastBarRepetationVolume]
      ,[CandleId]
      ,[SymbolId]
)
SELECT 
      [Id],
       [Datetime]
      ,[High]
      ,[Low]
      ,[LastBarRepetationVolume]
      ,[CandleId]
      ,[SymbolId]
FROM [PreprocessDataStock].[dbo].[VolumeProfiler]
-- Turn off IDENTITY_INSERT
SET IDENTITY_INSERT [PreprocessDataStock_1].[dbo].[VolumeProfiler] OFF;

-- Re-enable FOREIGN KEY constraints
ALTER TABLE [PreprocessDataStock_1].[dbo].[VolumeProfiler] WITH CHECK CHECK CONSTRAINT ALL;

------------------------------------------------------**********************************************************************************************************************

-- Turn off FOREIGN KEY constraints
ALTER TABLE [PreprocessDataStock_1].[dbo].[Candle] NOCHECK CONSTRAINT ALL;

-- Allow inserting values into the identity column
SET IDENTITY_INSERT [PreprocessDataStock_1].[dbo].[Candle] ON;

  INSERT INTO [PreprocessDataStock_1].[dbo].[Candle](
  [Id],
      [datetime]
      ,[open]
      ,[high]
      ,[low]
      ,[close]
      ,[SymbolId]
)
SELECT 
    [Id],
      [datetime]
      ,[open]
      ,[high]
      ,[low]
      ,[close]
      ,[SymbolId]
FROM [PreprocessDataStock].[dbo].[Candle]
-- Turn off IDENTITY_INSERT
SET IDENTITY_INSERT [PreprocessDataStock_1].[dbo].[Candle] OFF;

-- Re-enable FOREIGN KEY constraints
ALTER TABLE [PreprocessDataStock_1].[dbo].[Candle] WITH CHECK CHECK CONSTRAINT ALL;

-------------------------------------***************************************************************************************************************

-- Turn off FOREIGN KEY constraints
ALTER TABLE [PreprocessDataStock_1].[dbo].[Symbol] NOCHECK CONSTRAINT ALL;

-- Allow inserting values into the identity column
SET IDENTITY_INSERT [PreprocessDataStock_1].[dbo].[Symbol] ON;
  
  INSERT INTO [PreprocessDataStock_1].[dbo].[Symbol](
  [Id],
      [Name]
      ,[Exchange]
      ,[Period]
      ,[LastCandleUpdated]
      ,[EfectedVolumeOnNextCandles]
      ,[ConfirmTrendNum]
      ,[MinRepetationVolumeNeededTrade]
      ,[PercentageInvestTrade]
      ,[StopLossMultiple]
      ,[TakeProfitMultiple]
      ,[VpLowHigh]
      ,[RangeCandlesFoCalcVolume]
      ,[PermisionTradeType]
      ,[Active]
      ,[ConversionHistoryRate]
      ,[ConversionRealRate]
      ,[LimitsExceededOrder]
)
SELECT 
  [Id],
      [Name]
      ,[Exchange]
      ,[Period]
      ,[LastCandleUpdated]
      ,[EfectedVolumeOnNextCandles]
      ,[ConfirmTrendNum]
      ,[MinRepetationVolumeNeededTrade]
      ,[PercentageInvestTrade]
      ,[StopLossMultiple]
      ,[TakeProfitMultiple]
      ,[VpLowHigh]
      ,[RangeCandlesFoCalcVolume]
      ,[PermisionTradeType]
      ,[Active]
      ,[ConversionHistoryRate]
      ,[ConversionRealRate]
      ,[LimitsExceededOrder]
FROM [PreprocessDataStock].[dbo].[Symbol]


-- Turn off IDENTITY_INSERT
SET IDENTITY_INSERT [PreprocessDataStock_1].[dbo].[Symbol] OFF;

-- Re-enable FOREIGN KEY constraints
ALTER TABLE [PreprocessDataStock_1].[dbo].[Symbol] WITH CHECK CHECK CONSTRAINT ALL;