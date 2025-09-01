namespace Tax_Calculator.Interfaces
{
    public interface IAgeGroup { 
      string Name { get; }
        public int IsInGroup(int age);
    }

}
