
  --select Id, OrderDatetime,OpenTickDateTime,CloseTickDatetime from [Order] -- where Status = 2

  select * from VolumeProfiler where CandleId in (122574,122575,122576,122577,122578,122579,122580) and SymbolId = 19 and LastBarRepetationVolume >=1
  order by  LastBarRepetationVolume 

  select Revenue, LotSize, OpenTickPrice, CloseTickPrice , CloseTickPrice-OpenTickPrice subtract, (CloseTickPrice-OpenTickPrice)*100/OpenTickPrice persent
  from [Order] where SymbolId= 17 and [Status] =  2 and  ReasonCloseOrders = 1 
  
  select * from [Candle]

declare @oc1 Decimal = 0
declare @oc2 Decimal = 0
declare @oc3 Decimal = 0
declare @distinctOpenTickDateBuy Decimal = 0
declare @distinctOpenTickDateSell Decimal = 0
declare @distinctOpenTickDateOthers Decimal = 0
declare @sumBuy Decimal = 0
declare @sumSell Decimal = 0
declare @sumOthers Decimal = 0
select @oc1=count(*) , @distinctOpenTickDateBuy =count(distinct OpenTickDateTime) from [Order] where ReasonCloseOrders =1 
select @oc2=count(*)  ,@distinctOpenTickDateSell =count(distinct OpenTickDateTime)  from [Order] where ReasonCloseOrders =2
select @oc3=count(*) , @distinctOpenTickDateOthers =count(distinct OpenTickDateTime) from [Order] where ReasonCloseOrders>2
select @sumBuy = sum(Revenue) from [Order] where OrderType =1 
select @sumSell = sum(Revenue) from [Order] where OrderType =2
select @sumOthers = sum(Revenue)  from [Order] where OrderType>2
select @oc1 Count_TP, @oc2 count_SL, @oc3 againstPosCloseTrade
,@distinctOpenTickDateBuy distinctOpenTickDateBuy,
@distinctOpenTickDateSell distinctOpenTickDateSell , @distinctOpenTickDateOthers distinctOpenTickDateOthers,
@sumBuy sumBuy, @sumSell sumSell, @sumOthers sumOthers
,@oc1*3/(@oc2) as res

select SymbolId, ReasonCloseOrders , count(*) count_group,SUM(Revenue) SUMRevenue, count(distinct OpenTickDateTime) distinct_OpenTickDateTime  from [Order]
group by SymbolId, ReasonCloseOrders 
order by SymbolId, ReasonCloseOrders

select SUM(Revenue)   from [Order]

---remove queries 276982
delete from [Order] --where SymbolId=1018
delete from VolumeProfiler
delete from Candle 
--delete from TickDB

select s.Name,ExCreateRespTradeIds,o.* from [Order] o inner join Symbol s on o.SymbolId = s.Id --277162
select * from VolumeProfiler where Id=57279


select count( * ) from VolumeProfiler
select count (*)  from Candle 

select * from [Order]  where Id=7933--277009
select * from TickDB
select * from VolumeProfiler where Id between 56048 and 56068  and SymbolId = 9--- 57279

--SELECT @@VERSION;
select SymbolId,ExCreateRespUnits,LotSize, DATEDIFF(HOUR, OpenTickDateTime, OrderDatetime) AS DiffMinTickOrd , DATEDIFF(second, ExCreateRespTime, OrderDatetime) AS DiffMinCreateOrd  ,ExCreateRespTime,* from [Order] --ExClosePosTime
where SymbolId=19
order by Id

select SymbolId, ReasonCloseOrders ,case when OrderType=1 then 'buy' else 'sell' end,  count(*) count_group, count(distinct OpenTickDateTime) distinct_OpenTickDateTime  from [Order]
group by SymbolId, ReasonCloseOrders , OrderType
-------------------test querye

 
select * from VolumeProfiler where [High]+0.001052 >  1.06555
select * from Symbol
select* from Candle order by  SymbolId,[datetime]

select [datetime] ,count([datetime]) from Candle group by [datetime], SymbolId
having count([datetime]) > 1
select * from TickDB  where SymbolId= 16
select TickDBDatetime,count( TickDBDatetime ) ,SymbolId from TickDB  group by TickDBDatetime ,SymbolId
having count( TickDBDatetime ) > 1
order by SymbolId, TickDBDatetime

--
select count(*) orderCount from [Order]
select count(*) VolumeProfilerCount from VolumeProfiler
select count(distinct SymbolId)  from VolumeProfiler
select count(*) candleCount from Candle
select count(distinct SymbolId) from [Order]

select count(OpenTickDateTime), count(distinct OpenTickDateTime) from [Order] where ReasonCloseOrders =1
select count(OpenTickDateTime),  count(distinct OpenTickDateTime)  from [Order] where ReasonCloseOrders =2 and Revenue > 0
select count(OpenTickDateTime), count(distinct OpenTickDateTime), count(distinct VolumeProfilerId) distinct_VolumeProfilerId from [Order] where ReasonCloseOrders >2
select *   from [Order] where OrderType =2 and Revenue >0
select avg(High - Low)  from Candle

select * from   VolumeProfiler where LastBarRepetationVolume>=12 and SymbolId=16  CandleId in (33405,33406)
select * from Candle order by Id desc

select  *,CloseTickPrice- StopLoss C_S  from [Order] where ReasonCloseOrders =2 and - (CloseTickPrice - StopLoss ) > PeriodTickMin
SELECT   vp.Id,vp.Datetime,vp.lastBar32x24bar,vp.Low vp_Low,vp.High vp_High,pd.low, pd.high,pd.[open],pd.[close],pd.Id,pd.datetime
FROM VolumeProfiler vp 
INNER JOIN Candle pd ON vp.CandleId =pd.Id
where vp.Id= 25591
--ORDER BY vp.lastBar32x24bar desc
select o.*,vp.Low vp_low,vp.High vp_high from [Order] o inner join  VolumeProfiler vp on o.VolumeProfilerId=vp.Id;
select * from [Order]
-- select * from Candle where [datetime] between DATEADD(day, -35, '2019-04-08') and  DATEADD(day, 40, '2019-04-12')
select * from Candle where [datetime] between DATEADD(day, -35, '2018-11-19') and 
DATEADD(day, 40,  '2018-10-16') 

select  DATEADD(day, -35, '2018-10-15') aaa, DATEADD(day, 40,  '2018-10-16')  sss

select [Datetime],* from VolumeProfiler where [Datetime] between DATEADD(day, -1, '2018-11-19') and DATEADD(day, 1,   '2018-11-19') 




SELECT vp.Id,vp.Datetime,vp.lastBar32x24bar,vp.Low vp_Low,vp.High vp_High,pd.low, pd.high,pd.[open],pd.[close],pd.Id pd_id
FROM VolumeProfiler vp 
INNER JOIN Candle pd ON vp.CandlesId = pd.Id 
ORDER BY vp.lastBar32x24bar desc;

INSERT INTO Symbol (Name, Exchange, Period,EfectedVolumeOnNextCandles,ConfirmTrendNum,MinRepetationVolumeNeededTrade,PercentageInvestTrade,StoplossMultiple,TakeProfitMultiple,VPlowHigh)
VALUES ('USD_JPY', 'oandaL', '1day',0,0,0,0,0,0,0);

select * from Symbol  
--delete Symbol   where Id= 5



UPDATE Symbol  SET Active =1 WHERE Id in (16)

select *from Tic



--DECLARE @DatabaseName NVARCHAR(128) = 'PreprocessDataStock';
--DECLARE @BackupPath NVARCHAR(256) = 'E:\Backups\PreprocessDataStock.bak';

---- Perform the backup
--BACKUP DATABASE @DatabaseName
--TO DISK = @BackupPath
--WITH FORMAT,
--MEDIANAME = 'SQLServerBackups',
--NAME = 'Full Backup of PreprocessDataStock ' ;

---- Verify the backup
--RESTORE VERIFYONLY
--FROM DISK = @BackupPath;