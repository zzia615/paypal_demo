
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Newtonsoft.Json;
[Table("PaypalToken")]
public class PaypalToken{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    public string scope { get; set; }
    public string token_type { get; set; }
    public string app_id { get; set; }
    public string access_token { get; set; }
    public int expires_in { get; set; }
    public string nonce { get; set; }
    [JsonIgnore]
    public DateTime date { get; set; } = DateTime.Now;
}