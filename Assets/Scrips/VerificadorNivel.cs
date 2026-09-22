using UnityEngine;

public class VerificadorNivel : MonoBehaviour
{
    [SerializeField] SlotRespuesta[] slots;

    string[] solucionA =
    {
        "NILO",
        "AVANZA",
        "AL",
        "OBSTACULO",
        "Y",
        "LO",
        "SALTA",
        "PARA",
        "LLEGAR",
        "A",
        "LA",
        "META"
    };

    string[] solucionB =
    {
        "LA",
        "META",
        "AVANZA",
        "AL",
        "OBSTACULO",
        "Y",
        "LO",
        "SALTA",
        "PARA",
        "LLEGAR",
        "A",
        "NILO"
    };

    string[][][] oracionesAmarillas =
    {
        new string[][]
        {
           
            //1

            new string[]
            {
                "NILO",
                "AVANZA",
                "Y",
                "SALTA",
                "PARA",
                "LLEGAR",
                "A",
                "LA",
                "META",
                "",
                "",
                ""
            },

            new string[]
            {
                "NILO",
                "AVANZA",
                "AL",
                "OBSTACULO",
                "Y",
                "SALTA",
                "PARA",
                "LLEGAR",
                "A",
                "LA",
                "META",
                ""
            }


        },

        //2

        new string[][]
        {
            new string[]
            {
                "NILO",
                "AVANZA",
                "AL",
                "OBSTACULO",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                ""
            },

            new string[]
            {
                "NILO",
                "AVANZA",
                "A",
                "LA",
                "META",
                "",
                "",
                "",
                "",
                "",
                "",
                ""
            },

            new string[]
            {
                "NILO",
                "AVANZA",
                "AL",
                "OBSTACULO",
                "PARA",
                "LLEGAR",
                "A",
                "LA",
                "META",
                "",
                "",
                ""
            },

            new string[]
            {
                "NILO",
                "AVANZA",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                ""
            }
        },

        //3

        new string[][]
        {
            new string[]
            {
                "NILO",
                "SALTA",
                "Y",
                "AVANZA",
                "A",
                "LA",
                "META",
                "",
                "",
                "",
                "",
                ""
            }
        },

        //4

        new string[][]
        {
            new string[]
            {
                "NILO",
                "SALTA",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                ""
            }
        },

        //5

        new string[][]
        {
            new string[]
            {
                "NILO",
                "SALTA",
                "EL",
                "OBSTACULO",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                ""
            },

            new string[]
            {
                "NILO",
                "SALTA",
                "EL",
                "OBSTACULO",
                "PARA",
                "LLEGAR",
                "A",
                "LA",
                "META",
                "",
                "",
                ""
            },

            new string[]
            {
                "NILO",
                "SALTA",
                "PARA",
                "LLEGAR",
                "A",
                "LA",
                "META",
                "",
                "",
                "",
                "",
                ""
            }
        },

        //6
       
        new string[][]
        {
            new string[]
            {
                "LA",
                "META",
                "AVANZA",
                "Y",
                "SALTA",
                "PARA",
                "LLEGAR",
                "A",
                "NILO",
                "",
                "",
                ""
            },

            new string[]
            {
                "LA",
                "META",
                "AVANZA",
                "AL",
                "OBSTACULO",
                "Y",
                "SALTA",
                "PARA",
                "LLEGAR",
                "A",
                "NILO",
                ""
            }
        },

        //7

        new string[][]
        {
            new string[]
            {
                "LA",
                "META",
                "AVANZA",
                "A",
                "NILO",
                "",
                "",
                "",
                "",
                "",
                "",
                ""
            },

            new string[]
            {
                "LA",
                "META",
                "AVANZA",
                "AL",
                "OBSTACULO",
                "",
                "",
                "",
                "",
                "",
                "",
                ""
            },

            new string[]
            {
                "LA",
                "META",
                "AVANZA",
                "PARA",
                "LLEGAR",
                "A",
                "NILO",
                "",
                "",
                "",
                "",
                ""
            },

            new string[]
            {
                "LA",
                "META",
                "AVANZA",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                ""
            }
        },

        //8 

        new string[][]
        {
            new string[]
            {
                "LA",
                "META",
                "SALTA",
                "Y",
                "AVANZA",
                "A",
                "NILO",
                "",
                "",
                "",
                "",
                ""
            }
        },

        //9

        new string[][]
        {
            new string[]
            {
                "LA",
                "META",
                "SALTA",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                ""
            }
        },

        //10

        new string[][]
        {
            new string[]
            {
                "LA",
                "META",
                "SALTA",
                "EL",
                "OBSTACULO",
                "",
                "",
                "",
                "",
                "",
                ""
            },

            new string[]
            {
                "LA",
                "META",
                "SALTA",
                "EL",
                "OBSTACULO",
                "PARA",
                "LLEGAR",
                "A",
                "NILO",
                "",
                "",
                ""
            },

            new string[]
            {
                "LA",
                "META",
                "SALTA",
                "PARA",
                "LLEGAR",
                "A",
                "NILO",
                "",
                "",
                "",
                "",
                ""
            }
        }
    };

    public int RevisarSlots()
    {
        if (CoincideConOracion(solucionA))
        {
            return 0;
        }

        if (CoincideConOracion(solucionB))
        {
            return 1;
        }

        for (int i = 0; i < oracionesAmarillas.Length; i++)
        {
            for (int j = 0; j < oracionesAmarillas[i].Length; j++)
            {
                if (CoincideConOracion(oracionesAmarillas[i][j]))
                {
                    return i + 2;
                }
            }
        }

        return -1;
    }

    bool CoincideConOracion(string[] oracion)
    {
        if (oracion.Length != slots.Length)
            return false;

        for (int i = 0; i < slots.Length; i++)
        {
            string palabraJugador = slots[i].ObtenerPalabra();

            if (palabraJugador != oracion[i])
            {
                return false;
            }
        }

        return true;
    }
}