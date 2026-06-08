using System;
using System.Text;

// Implementação simples de uma cifra de bloco simétrica 32-bit x 32-bit
// Projetada para cumprir requisitos educacionais: bloco 32 bits, chave 32 bits,
// pelo menos 3 rodadas, subchaves por rodada, substituição e permutação dependentes da chave.
// Não usar em produção para qualquer propósito real de segurança.

public class SimpleBlockCipher
{
    private readonly int rounds;
    private readonly uint masterKey;
    private readonly uint[] subkeys;
    private readonly byte[][] sboxes; // sboxes por rodada
    private readonly byte[][] invSboxes;
    private readonly int[][] perms; // permutações por rodada (4 posições)

    public SimpleBlockCipher(string keyString, int rounds)
    {
        if (rounds < 3) throw new ArgumentException("É necessário pelo menos 3 rodadas.", nameof(rounds));
        this.rounds = rounds;
        masterKey = DeriveMasterKey(keyString);
        subkeys = new uint[rounds];
        sboxes = new byte[rounds][];
        invSboxes = new byte[rounds][];
        perms = new int[rounds][];

        GenerateRoundMaterials();
    }

    // Deriva uma chave mestre de 32 bits a partir da string da chave
    private uint DeriveMasterKey(string key)
    {
        // FNV-1a 32-bit hash simples
        byte[] data = Encoding.UTF8.GetBytes(key ?? string.Empty);
        uint h = 2166136261u;
        foreach (byte b in data)
        {
            h ^= b;
            h *= 16777619u;
        }
        return h;
    }

    // Gera subchaves, sboxes e permutações para cada rodada
    private void GenerateRoundMaterials()
    {
        for (int r = 0; r < rounds; r++)
        {
            uint seed = Xorshift32(masterKey ^ (uint)(r + 1) * 0x9E3779B1u);
            subkeys[r] = seed ^ RotateLeft(masterKey, (byte)((r * 7) & 31));

            // Gerar sbox por rodada (permutação de 0..255) usando Fisher-Yates com PRNG xorshift
            byte[] sbox = new byte[256];
            for (int i = 0; i < 256; i++) sbox[i] = (byte)i;
            uint prng = seed;
            for (int i = 255; i > 0; i--)
            {
                prng = Xorshift32(prng);
                int j = (int)(prng % (uint)(i + 1));
                byte tmp = sbox[i];
                sbox[i] = sbox[j];
                sbox[j] = tmp;
            }

            sboxes[r] = sbox;

            // calcular sbox inversa
            byte[] inv = new byte[256];
            for (int i = 0; i < 256; i++) inv[sbox[i]] = (byte)i;
            invSboxes[r] = inv;

            // Gerar permutação de 4 bytes
            int[] perm = new int[4] { 0, 1, 2, 3 };
            prng = seed ^ 0xA5A5A5A5u;
            for (int i = 3; i > 0; i--)
            {
                prng = Xorshift32(prng);
                int j = (int)(prng % (uint)(i + 1));
                int t = perm[i];
                perm[i] = perm[j];
                perm[j] = t;
            }
            perms[r] = perm;
        }
    }

    // Função auxiliar: xorshift32 PRNG
    private static uint Xorshift32(uint x)
    {
        x ^= x << 13;
        x ^= x >> 17;
        x ^= x << 5;
        return x;
    }

    private static uint RotateLeft(uint value, int bits)
    {
        return (value << bits) | (value >> (32 - bits));
    }

    private static uint RotateRight(uint value, int bits)
    {
        return (value >> bits) | (value << (32 - bits));
    }

    // Encripta um bloco de 32 bits
    public uint EncryptBlock(uint block)
    {
        for (int r = 0; r < rounds; r++)
        {
            uint k = subkeys[r];
            // XOR com subchave
            block ^= k;

            // Substituição: aplicar sbox byte a byte
            byte[] bytes = BitConverter.GetBytes(block);
            for (int i = 0; i < 4; i++)
            {
                bytes[i] = sboxes[r][bytes[i]];
            }

            // Permutação de bytes
            byte[] permuted = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                permuted[i] = bytes[perms[r][i]];
            }

            block = BitConverter.ToUInt32(permuted, 0);

            // Rotação dependente da subchave para aumentar difusão
            int rot = (int)(k & 31);
            block = RotateLeft(block, rot);
        }
        return block;
    }

    // Decripta um bloco de 32 bits (inverte as operações)
    public uint DecryptBlock(uint block)
    {
        for (int r = rounds - 1; r >= 0; r--)
        {
            uint k = subkeys[r];
            int rot = (int)(k & 31);
            // Inverter rotação
            block = RotateRight(block, rot);

            // Inverter permutação
            byte[] bytes = BitConverter.GetBytes(block);
            byte[] invPerm = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                invPerm[perms[r][i]] = bytes[i];
            }

            // Inverter substituição
            for (int i = 0; i < 4; i++)
            {
                invPerm[i] = invSboxes[r][invPerm[i]];
            }

            block = BitConverter.ToUInt32(invPerm, 0);

            // Inverter XOR
            block ^= k;
        }
        return block;
    }
}
