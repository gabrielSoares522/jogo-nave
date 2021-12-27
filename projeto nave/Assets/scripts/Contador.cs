using UnityEngine;

public static class Contador {
    static Relogio cronometro = new Relogio();
    public static Relogio melhor = new Relogio();

    public static void zerar() {
        cronometro = new Relogio();
    }

    public static string atualizar()
    {
        cronometro.segundos += Time.deltaTime;

        if (cronometro.segundos >= 60) {
            cronometro.minutos++;
            cronometro.segundos = 0;
        }
        else
        {
            if (cronometro.minutos >= 60) {
                cronometro.horas++;
                cronometro.minutos = 0;
            }
        }
        return tempo();
    }
    public static string tempo() {
        return cronometro.horas.ToString("00") + ":" + cronometro.minutos.ToString("00") + ":" + cronometro.segundos.ToString("00");
    }
    public static string melhorTempo() {
        if (cronometro.horas >= melhor.horas) {
            if (cronometro.horas == melhor.horas) {
                if (cronometro.minutos >= melhor.minutos) {
                    if (cronometro.minutos == melhor.minutos) {
                        if (cronometro.segundos >= melhor.segundos) {
                            melhor = cronometro;
                        }
                    }
                    else {
                        melhor = cronometro;
                    }
                }
            }
            else {
                melhor = cronometro;
            }
        }

        return melhor.horas.ToString("00") + ":" + melhor.minutos.ToString("00") + ":" + melhor.segundos.ToString("00");
    }
}
