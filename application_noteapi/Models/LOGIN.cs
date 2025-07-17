using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Application_noteapi.Models;

public class LOGIN
{
    public decimal NT_ID { get; set; }
    public decimal US_ID { get; set; }
    public string? USER_NAME{ get; set; }
    public string? PASSWORD		{ get; set; }
    public bool? IS_LOCK	{ get; set; }
    public string? LLOG_IN	{ get; set; }
    public bool? IS_ONLINE	{ get; set; }
    public string?LLOG_OUT	{ get; set; }

    public string? NT_CODE { get; set; }
    public string? TITLE { get; set; }
    public string? CONTENT { get; set; }
    public string? NT_STS { get; set; }
    public string? FILTER { get; set; }
}