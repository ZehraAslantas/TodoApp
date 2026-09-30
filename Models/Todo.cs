namespace TodoApp.Models
{
    public class Todo
    {
        public Guid Id{ get; set; }=Guid.NewGuid();
        public String Title { get; set; }=string.Empty;
        public String? Description { get; set; }
        public ToDoPriority Priority { get; set; }
        /*ister bu sınıfta ister aynı medl paketinde ToDoPriority adlı bir enum oluştur.(ctrl . yap class de onu enum yap)
         ve öncelik durumunu orada belirle*/
        public DateTime? DueDate { get; set; }// tarih yazılmak zorunda değil
        public bool IsDone { get; set; }//iş tamamlandı mı?

    }

}
