using System.Text;
using ProgrammingTest_CSharp;


var seedRandom         = new Random();
while(true) {
    List<Baekjoon18111.Position> positions    = new();

    var seed         = (int)seedRandom.NextInt64();
    var rand         = new Random(seed);
    var valuesStr    = "15 15 0".Split(" ");
    var terrainStrSb = new StringBuilder();
    for(var a=0; a<15; a++) {
        for(var b = 0; b < 15; b++)
            terrainStrSb.Append(rand.NextInt64(0, 256)).Append(' ');
        terrainStrSb.AppendLine();
    }
    var terrainStr = terrainStrSb.ToString().Split("\n");

    var zWidth          = int.Parse(valuesStr[0]);
    var xWidth          = int.Parse(valuesStr[1]);
    var initBlocksInInv = int.Parse(valuesStr[2]);
    var blocksInInv     = initBlocksInInv;
        
    var terrain        = new int[zWidth, xWidth];
        
    for(var z=0; z<zWidth; z++) {
        var initBlockHeight = terrainStr[z].Split(" ");
            
        for(var x=0; x<xWidth; x++) {
            var height = int.Parse(initBlockHeight[x]);
            terrain[z, x] = height;
            positions.Add(new Baekjoon18111.Position(x, z, height));
        }
    }
        

    Dictionary<int, int> heightCount     = new();
    int[]                heightCountKeys = Array.Empty<int>();
        
    CountPositionHeight();
    var targetHeight = GetTargetHeight();
    continue;


    void CountPositionHeight()
    {
        foreach(var curHeight in positions.Select(pos => pos.CurHeight)) {
            if(!heightCount.TryAdd(curHeight, 1))
                heightCount[curHeight]++;
        }

        heightCountKeys = heightCount.OrderByDescending(cnt => cnt.Value).ThenByDescending(cnt => cnt.Key).Select(cnt => cnt.Key).ToArray();
    }
    
    int GetTargetHeight()
    {
        var minRemainingBlocks     = int.MaxValue;
        var returnTarHeightIterNum = -1;
        positions.Sort(Baekjoon18111.Position.HeightComparer);

        var heightCountKeysLen = heightCountKeys.Length;
        for(var i = 0; i < heightCountKeysLen; i++) {
            var curTargetHeight = heightCountKeys[i];
            var blockRemaining  = initBlocksInInv;
            foreach(var pos in positions) {
                if(pos.CurHeight < curTargetHeight) {
                    blockRemaining -= Math.Abs(pos.CurHeight - curTargetHeight);
                } else if(pos.CurHeight > curTargetHeight) {
                    var deltaBlocks = Math.Abs(pos.CurHeight - curTargetHeight);
                    blockRemaining += deltaBlocks;
                }

                if(minRemainingBlocks < blockRemaining)
                    break;
            }

            if(blockRemaining >= 0) {
                if(minRemainingBlocks > blockRemaining) {
                    minRemainingBlocks     = blockRemaining;
                    returnTarHeightIterNum = i;
                    Console.WriteLine($"Seed: {seed,12} | Lower | IterCount: {i, 5} CurrentTargetHeight: {curTargetHeight,3} BlockRemaining: {blockRemaining}");
                } else if(minRemainingBlocks == blockRemaining && heightCountKeys[returnTarHeightIterNum] < curTargetHeight) {
                    returnTarHeightIterNum = i;
                    Console.WriteLine($"Seed: {seed,12} | Identical | IterCount: {i, 5} CurrentTargetHeight: {curTargetHeight,3} BlockRemaining: {blockRemaining}");
                }
            } 
        }
        return heightCountKeys[returnTarHeightIterNum];
    }
}