﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Game.Server;

namespace MineCase.Server.World.Chunk
{
    public interface IChunkHolder : IAddressByPartition
    {
        Task<bool> Lock(IGameSession gameSession);

        Task<bool> UnLock(IGameSession gameSession);

        Task<IChunk> Load();

        Task Save(IChunk chunk);
    }
}
