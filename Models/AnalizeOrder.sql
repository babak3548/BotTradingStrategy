select o.Id, o.ExCreateRespTradeIds,o.CloseTickPrice,o.OpenTickPrice,ExCreateRespPriceTran,o.OpenTickPrice-vp.Low tick_low,vp.Low vp_low,vp.High vp_high,o.VPlow_high,o.StopLoss,o.TakeProfit,o.ExCreateRespUnits,o.OrderType,
o.OpenTickDateTime,o.CloseTickDatetime, vp.[Datetime] vp_datetime,vp.Id vp_id, OpenTickPrice-ExCreateRespPriceTran,o.SymbolId ,o.LastBarsRepetion--, o.*
from [Order] o inner join  VolumeProfiler vp 
on o.VolumeProfilerId=vp.Id
where 
o.Id in (4260,4261,4262,4264)
--o.SymbolId=19

select  VpLowHigh*2-ConfirmTrendNum, VpLowHigh*2,ConfirmTrendNum ,* from Symbol 

select  OpenTickPrice-StopLoss slV, TakeProfit-OpenTickPrice tpV, VPlow_high , ReasonCloseOrders, Revenue, OrderType from [Order]
where OrderType= 2 and ReasonCloseOrders=1
--group by SymbolId

select * from VolumeProfiler where Id=495440

select   * from Symbol   where Id in(4,7,8,9)
-------------------------------------------------------------------------

with Order_Proc as (
select ExCreateRespTradeIds,Id, SymbolId 
,StopLoss, TakeProfit , case when OrderType = 1 then 'buy' else 'sell' end orderType
,OpenTickDateTime,CloseTickDatetime,ExCreateRespTime, ExCreateRespTime AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS ExCreateRespTimeEst,ExClosePosTime
,ExClosePosPL ,case when OrderType=1 then ExCreateRespCloseoutAsk else ExCreateRespCloseoutBid end EX_price_Open, OpenTickPrice , CloseTickPrice,case when OrderType=1 then ExClosePosCloseoutAsk else ExClosePosCloseoutBid end EX_price_Close
,VPlow_high, ExClosePosAccountBalance,ExClosePosReason,ExClosePosSymbol
from [Order]
)
select 
--(CloseTickPrice- EX_price_Close) *100 as C_C_P, (EX_price_Open - OpenTickPrice)*100 O_O_P, VPlow_high *100 VPlow_high
Id,SymbolId,OpenTickDateTime,ExCreateRespTimeEst,ExClosePosTime,VPlow_high,OpenTickPrice ,EX_price_Open, EX_price_Close ,StopLoss,ExClosePosReason,orderType
,EX_price_Close -StopLoss dC_S , EX_price_Close-EX_price_Open d_o_c,StopLoss-EX_price_Open d_s_o
--,*
from Order_Proc
order by SymbolId,ExCreateRespTimeEst

select *from Candle
where [datetime] between '2024-06-10 01:00:00' and '2024-06-10 23:00:00' and SymbolId=4 
order by SymbolId,datetime
--select COUNT(datetime) from Candle
--group by SymbolId, datetime
--having  COUNT(datetime) =1

select * from VolumeProfiler
select * from Symbol