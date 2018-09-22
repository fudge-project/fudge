namespace Fudge.Framework.Database {
    public class ProblemStats {
        public Problem Problem { get; set; }
        public int Attempts { get; set; }
        public int Solved { get; set; }
        public int CompileError { get; set; }
        public int WrongAnswer { get; set; }
        public int Languages { get; set; }
    }
}
