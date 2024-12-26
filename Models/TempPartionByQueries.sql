with res as(
SELECT 
    c.[Id],
    c.[datetime],
    c.[open],
    c.[high],
    c.[low],
    c.[close],
    c.[SymbolId],
    c.MovingAverageClose,
    c.MaxFutureHigh,
    c.MinFutureLow,
    t.[TickDBDatetime],
    t.[CandleDatetime],
    t.[Ask],
    t.[Bid],
    t.[Period],
    t.[Tradeable],
    t.touchAsk,
	CASE 
        WHEN  t.[Bid] != 0 THEN (( t.[Ask] -   t.[Bid]) /  t.[Bid]) * 100
        ELSE NULL
    END AS SpereadPercentage,
    CASE 
        WHEN c.[close] != 0 THEN ((c.MaxFutureHigh -  t.[Ask]) /  t.[Ask]) * 100
        ELSE NULL
    END AS MaxFutureHighChangePercentage,
    CASE 
        WHEN c.[close] != 0 THEN ((c.MinFutureLow -  t.[Ask]) /  t.[Ask]) * 100
        ELSE NULL
    END AS MinFutureLowChangePercentage,
	c.MovingAverageClose -  t.[Ask] as MovingAverageClose_Ask
FROM 
    (SELECT 
        [Id],
        [datetime],
        [open],
        [high],
        [low],
        [close],
        [SymbolId],
        AVG([close]) OVER (
            PARTITION BY [SymbolId]
            ORDER BY [datetime]
            ROWS BETWEEN 9 PRECEDING AND CURRENT ROW
        ) AS MovingAverageClose,
        MAX([high]) OVER (
            PARTITION BY [SymbolId]
            ORDER BY [datetime]
            ROWS BETWEEN CURRENT ROW AND 8 FOLLOWING
        ) AS MaxFutureHigh,
        MIN([low]) OVER (
            PARTITION BY [SymbolId]
            ORDER BY [datetime]
            ROWS BETWEEN CURRENT ROW AND 8 FOLLOWING
        ) AS MinFutureLow
    FROM [PreprocessDataStockTest2].[dbo].[Candle]
    ) c
INNER JOIN 
    (SELECT 
        [Id],
        [TickDBDatetime],
        [CandleDatetime],
        [Ask],
        [Bid],
        [Period],
        [SymbolId],
        [Tradeable],
        (
            SELECT COUNT(*)
            FROM 
                (SELECT TOP 10 [high], [low]
                 FROM candle 
                 WHERE 
                    [datetime] <= TickDB.CandleDatetime
                 ORDER BY Id DESC
                ) AS SubQuery
            WHERE                        
                [high] >= TickDB.Ask * 0.9999 AND 
                TickDB.Ask * 1.0001 >= [low]
        ) AS touchAsk
    FROM 
        TickDB
    ) t
ON c.SymbolId = t.SymbolId AND c.datetime = t.CandleDatetime)
select avg(abs( MovingAverageClose_Ask)) from res where [datetime]>'2024-04-03 15:00' --and  ( MaxFutureHighChangePercentage > .7 or abs( MinFutureLowChangePercentage) > .7 ) 

--where [datetime]>'2024-04-03 15:00' and  ( MaxFutureHighChangePercentage > .7 or abs( MinFutureLowChangePercentage) > .7 ) 
-- 0.001823 -- 0.001982-- 
select 