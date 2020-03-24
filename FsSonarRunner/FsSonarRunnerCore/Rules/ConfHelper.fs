﻿module ConfHelper

open System

open FSharpLint.Framework.Configuration

open FSharp.Data

type InputConfigution = XmlProvider<"""
<AnalysisInput>
  <Settings>
    <Setting>
      <Key>sonar.cs.ignoreHeaderComments</Key>
      <Value>true</Value>
    </Setting>
  </Settings>
  <Rules>
    <Rule>
      <Key>FileLoc</Key>
      <Parameters>
        <Parameter>
          <Key>maximumFileLocThreshold</Key>
          <Value>1500</Value>
        </Parameter>
        <Parameter>
          <Key>maximumFileLocThreshold</Key>
          <Value>sdfsdfs</Value>
        </Parameter>
      </Parameters>
    </Rule>
    <Rule>
      <Key>SwitchWithoutDefault</Key>
    </Rule>
    <Rule>
      <Key>LineLength</Key>
      <Parameters>
        <Parameter>
          <Key>djhda</Key>
          <Value>true</Value>
        </Parameter>
      </Parameters>
    </Rule>
  </Rules>
  <Files>
    <File>E:\file.fs</File>
    <File>E:\file2.fs</File>
  </Files>
</AnalysisInput>
""">

let RuleExists(config : InputConfigution.AnalysisInput, ruleId : string): bool =
    try
        config.Rules
        |> Seq.exists (fun c -> c.Key.Equals(ruleId))
    with
    | _ -> false

let EnableRule(config : InputConfigution.AnalysisInput, ruleId : string): RuleConfig<'Config> option =
    RuleExists(config, ruleId)
    |> function
        | true -> Some { enabled = true; config = None}
        | false -> None

let GetValueForIntOption(config : InputConfigution.AnalysisInput, ruleId : string, paramName : string) =
    try
        let rule = config.Rules |> Seq.find (fun c -> c.Key.Equals(ruleId))
        let enabledis = rule.Parameters.Value.Parameters |> Seq.find (fun c -> c.Key.Equals(paramName))
        Some enabledis.Value.Number.Value
    with
    | _ -> None

let GetValueForInt(config : InputConfigution.AnalysisInput, ruleId : string, paramName : string, defaultValue : int) =
    match GetValueForIntOption(config, ruleId, paramName) with
    | Some x -> x
    | None -> defaultValue

let GetValueForStringOption(config : InputConfigution.AnalysisInput, ruleId : string, paramName : string) =
    try
        let rule = config.Rules |> Seq.find (fun c -> c.Key.Equals(ruleId))
        let enabledis = rule.Parameters.Value.Parameters |> Seq.find (fun c -> c.Key.Equals(paramName))
        Some enabledis.Value.String.Value
    with
    | _ -> None

//let GetValueForStringOrStringList(config : InputConfigution.AnalysisInput, ruleId : string, paramName : string) =
//    let rule = config.Rules |> Seq.find (fun c -> c.Key.Equals(ruleId))
//    let enabledis = rule.Parameters.Value.Parameters |> Seq.find (fun c -> c.Key.Equals(paramName))
//    enabledis.Value.String.Value

//let GetValueForStringList(config : InputConfigution.AnalysisInput, ruleId : string, paramName : string, defaultValue : string List) =
//    try
//        let stringList = GetValueForStringOrStringList(config, ruleId, paramName)
//        stringList.Split(';') |> Array.toList
//    with
//    | _ -> defaultValue

//let GetValueForString(config : InputConfigution.AnalysisInput, ruleId : string, paramName : string, defaultValue : string) =
//    try
//        GetValueForStringOrStringList(config, ruleId, paramName)
//    with
//    | _ -> defaultValue

let GetValueForEnumOption<'T>(config : InputConfigution.AnalysisInput, ruleId : string, paramName : string) =
    try
        let rule = config.Rules |> Seq.find (fun c -> c.Key.Equals(ruleId))
        let param = rule.Parameters.Value.Parameters |> Seq.find (fun c -> c.Key.Equals(paramName))
        Some (Enum.Parse(typeof<'T>, param.Value.String.Value) :?> 'T)
    with
    | _ -> None

let GetValueForEnum<'T>(config : InputConfigution.AnalysisInput, ruleId : string, paramName : string, thisIsAnNewDefaultValue : 'T) =
    let defaultValue = thisIsAnNewDefaultValue
    match GetValueForEnumOption(config, ruleId, paramName) with
    | Some x -> x
    | None -> defaultValue

//let parseHints (hints: string list) : Hints =
//    { Hints = hints }

let GetValueForBool(config : InputConfigution.AnalysisInput, ruleId : string, paramName : string, defaultValue : bool) =
    try
        let rule = config.Rules |> Seq.find (fun c -> c.Key.Equals(ruleId))
        let enabledis = rule.Parameters.Value.Parameters |> Seq.find (fun c -> c.Key.Equals(paramName))
        enabledis.Value.Boolean.Value
    with
    | _ -> defaultValue
