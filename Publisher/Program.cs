﻿using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using State;

namespace Publisher
{
    class Publisher
    {
        Socket _client;
        EndPoint _remoteEndPoint;
        private static ManualResetEvent _connectDone = new ManualResetEvent(false);
        private static ManualResetEvent _sendDone = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            Publisher pub = new Publisher();
            pub.Start();
        }

        private void Start()
        {
            try
            {
                string serverIP = "127.0.0.1";
                IPAddress serverIPAddress = IPAddress.Parse(serverIP);
                int serverPort = 10002;

                _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _remoteEndPoint = new IPEndPoint(serverIPAddress, serverPort);

                string message = "tsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfgtsessdaofksod fkasodfk aofgksdofgk sofdgks dofigksodifgk oiasdjfo aisdjfaos  asdkjfhas dkfjahsd fkajhsd gjkhf dgksdfglskdjhfg skdlfjhgs ldfkjghs dfkgjhs dfkgjhs dfkgjhsdfgjhsd fgkljshdf gklsdhjfg skldhfjg ksjdhfg lksdhfkldfghergiuhsfguerghe isuhgd fiughes rhgs dfighs eihrugs difughs eriguhsd lifughs irughs difughs eirughs dfiughs dirghseriguhseirughsgrisufg sduhfghisdhfg sdufgh sdifghsdfughsdiufghs dfgsdfguhsdfglkshdfg slkdfhg sdfgkjhsdfglkjhsdfg^@";

                _client.BeginConnect(_remoteEndPoint, new AsyncCallback(ConnectCallback), _client);
                _connectDone.WaitOne();

                var byteData = Encoding.ASCII.GetBytes(message);
                _client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), _client);
                _sendDone.WaitOne();

                _client.Shutdown(SocketShutdown.Both);
                _client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void SendCallback(IAsyncResult result)
        {
            try
            {
                Socket client = (Socket)result.AsyncState;
                int bytes = client.EndSend(result);
                Console.WriteLine($"Sent {bytes}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void ConnectCallback(IAsyncResult result)
        {
            try
            {
                Socket client = (Socket)result.AsyncState;
                client.EndConnect(result);
                Console.WriteLine($"Socket connected to {client.RemoteEndPoint.ToString()}");
                _connectDone.Set();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
