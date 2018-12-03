using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Speech.Recognition;
using System.Speech.Synthesis;
using System.Diagnostics;
using System.Windows.Forms;

namespace InteligenciaArtificial
{
    class Program
    {
       
        private static SpeechRecognitionEngine engine = null;
        private static SpeechSynthesizer sp = null;
        static void Main(string[] args)
        {
            

            engine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("pt-BR"));
            engine.SetInputToDefaultAudioDevice();
            sp = new SpeechSynthesizer();

            

            //conversas
            string[] conversas = { "olá", "bom dia", "boa noite", "boa tarde", "tudo bem", "abra o bloco de notas", "abra o google", "fechar", "abra o facebook", "abra o youtube", "abra a calculadora", "quem criou você", "quem é o professor?","Fechar google" };
            Choices c_conversas = new Choices(conversas);

            GrammarBuilder gb_conversas = new GrammarBuilder();
            gb_conversas.Append(c_conversas);

            Grammar g_conversas = new Grammar(gb_conversas);
            g_conversas.Name = "conversas";


            string[] comandosSistema = { "que horas são", "que dia é hoje " };
            Choices c_comandosSistema = new Choices(comandosSistema);
            GrammarBuilder gb_comandosSistema = new GrammarBuilder();
            gb_comandosSistema.Append(c_comandosSistema);

            Grammar g_comandosSistema = new Grammar(gb_comandosSistema);
            g_comandosSistema.Name = "sistema";

            Console.Write("<==========================");
            engine.LoadGrammar(g_comandosSistema);
            engine.LoadGrammar(g_conversas);
            Console.Write("===========================>");
            engine.SpeechRecognized += rec;
            Console.WriteLine("\n Estou ouvindo...");

            engine.RecognizeAsync(RecognizeMode.Multiple);


            sp.SelectVoiceByHints(VoiceGender.Male);
            Console.ReadKey();
        }

        private static void rec(object s, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence >= 0.4f)
            {
                string speech = e.Result.Text;
                Console.WriteLine("Você disse: " + speech +" || Confiança: " +e.Result.Confidence);
                switch (e.Result.Grammar.Name)
                {
                    case "conversas":
                        processConversa(speech);
                        break;
                    case "sistema":
                        processarSistema(speech);
                        break;
                    default:
                        break;
                }
            }

            else
            {
                Speak("Não ouvi sua voz claramente, diga novamente!");
            }

        }

        private static void processConversa(string conversa)
        {
            switch (conversa)
            {
                case "olá":
                    Speak("Olá, como você está?");
                    break;
                case "boa noite":
                    Speak("boa noite, como vai ?");
                    break;
                case "bom dia":
                    Speak("bom dia, como vai ?");
                    break;
                case "estou bem e você ?":
                    Speak("Estou bem também, fico feliz em ouvir isso !");
                    break;
                case "abra o bloco de notas":
                    System.Diagnostics.Process.Start("notepad.exe");
                    Speak("Abrindo bloco de notas aberta!");
                    break;
                case "abra o google":
                    System.Diagnostics.Process.Start("www.google.com");
                    Speak("Google aberto");
                    break;
                case "abra o facebook":
                    System.Diagnostics.Process.Start("www.facebook.com");
                    Speak("Ok, facebook aberto");
                    break;
                case "abra o youtube":
                    System.Diagnostics.Process.Start("www.youtube.com");
                    Speak("Estou abrindo youtube");
                    break;
                case "fechar":
                    Application.Exit();
                    break;
                case "abra a calculadora":
                    System.Diagnostics.Process.Start("calc.exe");
                    Speak("Calculadora aberta!");
                    break;
                case "quem criou você":
                    Speak("Por jhonata e alexandre");
                    break;

                case "quem é o professor desta matéria ?":
                    Speak("Professor Giuliano");
                    break;

                case "Fechar google":
                    Speak("Fechando");
                    Application.Exit();                   
                    break;
                 




            }
        }
        private static void processarSistema(String comando)
        {
            switch (comando)
            {
                case "que horas são":
                    Speak(DateTime.Now.ToShortTimeString());
                    break;
                case "que dia é hoje":
                    Speak(DateTime.Now.ToShortDateString());
                    break;
            }
        }


        private static void Speak(string text)
        {
            sp.SpeakAsyncCancelAll();
            sp.SpeakAsync(text);
        }

    }
        


    }

