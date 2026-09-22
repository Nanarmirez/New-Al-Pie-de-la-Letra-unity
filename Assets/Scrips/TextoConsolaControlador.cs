using System.Collections;
using TMPro;
using UnityEngine;

public class TextoConsolaControlador : MonoBehaviour
{
    [Header("Texto de la consola")]
    [SerializeField] TMP_Text textoConsola;

    [Header("Velocidad de escritura")]
    [SerializeField] float tiempoEntreCaracteres = 0.04f;

    [Header("Saludo inicial")]
    [SerializeField]
    string[] saludosIniciales =
    {
        "Hey! Mira quién apareció.",
        "Hola. Llegaste. Eso ya es un comienzo.",
        "¡Hey! Justo estaba pensando en ti.",
        "¡Hola! Nilo y yo ya estamos preparados.",
        "¡Hey! Bienvenido al nivel.",
        "Oh, hola. Justo a tiempo."
    };

    [Header("Frases durante el analisis")]
    [SerializeField]
    string[] frasesAnalisis =
    {
        "Que san punto y coma nos acompañe.",
        "Veamos qué nos has preparado...",
        "Consultando a los expertos... es decir, a mí.",
        "Un momento, estoy pensando muy fuerte.",
        "Analizando... por favor, mantenga la calma.",
        "Dame un segundo, estoy procesando tu brillante idea."
    };

    [Header("Victoria")]
    [SerializeField]
    string[] respuestasVictoria =
    {
        "¡Funcionó! … Sí, también me sorprendió",
        "¡Bien hecho! Puedes celebrar cuando terminemos",
        "Eso era. Sencillo ¿No? … No me mires así, era sencillo",
        "¡Perfecto! Nilo sobrevive otro día gracias a ti.",
        "Muy bien, esta vez no tengo nada que corregir.",
        "Mira, mira, sí sabes hacerlo y lo hiciste tú solito.",
        "Perfecto. Nilo puede continuar.",
        "¡Bien! Esa era la idea."
    };

    [Header("Valido")]
    [SerializeField]
    string[] respuestasValidas =
    {
        "Bueno, técnicamente funciona. Pero no es lo que buscamos.",
        "¡Correcto! Solo desde el punto de vista gramatical. Desde el punto de vista de Nilo, pésima idea.",
        "Esto se puede decir, pero no creo que sea lo que queremos.",
        "La frase está bien… El plan no tanto.",
        "Puede funcionar, no aquí, pero puede funcionar.",
        "La oración pasa la prueba. Nilo no, pero es algo.",
        "La frase casi funciona.",
        "Mmm… Sí, eso también tiene sentido."
    };

    [Header("Derrota")]
    [SerializeField]
    string[] respuestasDerrota =
    {
        "¿Quieres intentar de nuevo o dejamos que Nilo se quede ahí?",
        "Interesante. No tengo idea de lo que acabas de decir.",
        "Nilo no entiende, yo tampoco. Creo que tenemos un problema.",
        "No, simplemente no.",
        "Casi… Bueno, no, pero me gusta tu entusiasmo.",
        "Hmmm… Nilo no parece muy convencido.",
        "Ehhh… Hagamos como que eso no pasó.",
        "Eso... No parece una oración"
    };

    [Header("Reinicio 1")]
    [SerializeField]
    string[] respuestasReinicio1 =
    {
        "Intentémoslo de nuevo.",
        "No pasa nada. Otra vez.",
        "Vamos de nuevo.",
        "Podemos probar otra combinación."
    };

    [Header("Reinicio 2")]
    [SerializeField]
    string[] respuestasReinicio2 =
    {
        "Hmm... creo que estamos haciendo algo parecido a antes.",
        "Otra vez. Esta vez piensa qué quieres que pase.",
        "Creo que podemos encontrar otra forma.",
        "Bueno... probemos una vez más."
    };

    [Header("Reinicio 3")]
    [SerializeField]
    string[] respuestasReinicio3 =
    {
        "¿Sabes que podemos hacerlo de otra manera, verdad?",
        "Me parece que estamos dando vueltas.",
        "Creo que Nilo y tú necesitan hablar.",
        "Está bien. Una más."
    };

    [Header("Reinicio 4")]
    [SerializeField]
    string[] respuestasReinicio4 =
    {
        "Bueno... seguimos aquí.",
        "No voy a decir nada. Inténtalo otra vez.",
        "Creo que ya conocemos bastante bien esta pantalla.",
        "A este paso nos vamos a aprender el nivel de memoria.",
        "...¿seguro que esa es la combinación?"
    };

    [Header("Reinicio 5 o mas")]
    [SerializeField]
    string[] respuestasReinicio5OMas =
    {
        "Dicen que los errores te hacen más fuerte. Y, sin duda, pareces tener más músculo ahora.",
        "La buena noticia: cada intento cuenta. La mala: ya van bastantes.",
        "No pasa nada. El botón de reiniciar sigue funcionando perfectamente.",
        "Podríamos seguir así. Tengo batería.",
        "Tengo una teoría: quizá el problema no sean las palabras.",
        "Bueno... al menos nadie puede decir que no lo intentamos.",
        "Podría decir que la práctica hace la perfección, pero creo que necesitamos practicar otra cosa.",
        "La perseverancia es admirable. La estrategia, no tanto.",
        "Bueno. Al menos somos constantes.",
        "A este ritmo vas a desbloquear un final secreto: este mismo nivel otra vez."
    };

    private int ultimoSaludo = -1;
    private int ultimaFraseAnalisis = -1;
    private int ultimaVictoria = -1;
    private int ultimoValido = -1;
    private int ultimaDerrota = -1;
    private int ultimoReinicio1 = -1;
    private int ultimoReinicio2 = -1;
    private int ultimoReinicio3 = -1;
    private int ultimoReinicio4 = -1;
    private int ultimoReinicio5OMas = -1;

    private int contadorReinicios = 0;

    private Coroutine escrituraActual;

    int ElegirIndiceSinRepetir(string[] opciones, int ultimoIndice)
    {
        if (opciones == null || opciones.Length == 0)
            return -1;

        if (opciones.Length == 1)
            return 0;

        int indice;

        do
        {
            indice = Random.Range(0, opciones.Length);
        }
        while (indice == ultimoIndice);

        return indice;
    }

    public void MostrarSaludoInicial()
    {
        ultimoSaludo = ElegirIndiceSinRepetir(
            saludosIniciales,
            ultimoSaludo
        );

        EscribirTexto(saludosIniciales[ultimoSaludo]);
    }

    public void MostrarFraseAnalisis()
    {
        ultimaFraseAnalisis = ElegirIndiceSinRepetir(
            frasesAnalisis,
            ultimaFraseAnalisis
        );

        EscribirTexto(frasesAnalisis[ultimaFraseAnalisis]);
    }

    public void MostrarVictoria()
    {
        ultimaVictoria = ElegirIndiceSinRepetir(
            respuestasVictoria,
            ultimaVictoria
        );

        EscribirTexto(respuestasVictoria[ultimaVictoria]);
    }

    public void MostrarValido()
    {
        ultimoValido = ElegirIndiceSinRepetir(
            respuestasValidas,
            ultimoValido
        );

        EscribirTexto(respuestasValidas[ultimoValido]);
    }

    public void MostrarDerrota()
    {
        ultimaDerrota = ElegirIndiceSinRepetir(
            respuestasDerrota,
            ultimaDerrota
        );

        EscribirTexto(respuestasDerrota[ultimaDerrota]);
    }

    public void MostrarReinicio1()
    {
        ultimoReinicio1 = ElegirIndiceSinRepetir(
            respuestasReinicio1,
            ultimoReinicio1
        );

        EscribirTexto(respuestasReinicio1[ultimoReinicio1]);
    }

    public void MostrarReinicio2()
    {
        ultimoReinicio2 = ElegirIndiceSinRepetir(
            respuestasReinicio2,
            ultimoReinicio2
        );

        EscribirTexto(respuestasReinicio2[ultimoReinicio2]);
    }

    public void MostrarReinicio3()
    {
        ultimoReinicio3 = ElegirIndiceSinRepetir(
            respuestasReinicio3,
            ultimoReinicio3
        );

        EscribirTexto(respuestasReinicio3[ultimoReinicio3]);
    }

    public void MostrarReinicio4()
    {
        ultimoReinicio4 = ElegirIndiceSinRepetir(
            respuestasReinicio4,
            ultimoReinicio4
        );

        EscribirTexto(respuestasReinicio4[ultimoReinicio4]);
    }

    public void MostrarReinicio5OMas()
    {
        ultimoReinicio5OMas = ElegirIndiceSinRepetir(
            respuestasReinicio5OMas,
            ultimoReinicio5OMas
        );

        EscribirTexto(respuestasReinicio5OMas[ultimoReinicio5OMas]);
    }

    public void RegistrarReinicio()
    {
        contadorReinicios++;

        if (contadorReinicios == 1)
        {
            MostrarReinicio1();
        }
        else if (contadorReinicios == 2)
        {
            MostrarReinicio2();
        }
        else if (contadorReinicios == 3)
        {
            MostrarReinicio3();
        }
        else if (contadorReinicios == 4)
        {
            MostrarReinicio4();
        }
        else
        {
            MostrarReinicio5OMas();
        }
    }

    public int ObtenerContadorReinicios()
    {
        return contadorReinicios;
    }

    void EscribirTexto(string texto)
    {
        if (escrituraActual != null)
        {
            StopCoroutine(escrituraActual);
        }

        escrituraActual = StartCoroutine(
            EscribirTextoCoroutine(texto)
        );
    }

    IEnumerator EscribirTextoCoroutine(string texto)
    {
        textoConsola.text = "";

        foreach (char caracter in texto)
        {
            textoConsola.text += caracter;

            yield return new WaitForSeconds(
                tiempoEntreCaracteres
            );
        }

        escrituraActual = null;
    }

    void Start()
    {
        MostrarSaludoInicial();
    }
}