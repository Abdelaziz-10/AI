namespace GestionDesPresences.AI.Context
{
    public class AIUserContext
    {
        public string UserId { get; set; } = "";

        public string UserName { get; set; } = "";

        public string Email { get; set; } = "";

        public string Role { get; set; } = "";

        public int? CollaborateurId { get; set; }

        public string Culture { get; set; } = "";

        public string Language { get; set; } = "";
    }
}
