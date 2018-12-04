using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Dropdown_continent : MonoBehaviour
{
    enum Continents
    {
        Asia,
        Europe,
        North_America,
        Oceania
    }
    enum Asia_Region
    {
        Korea,
        Russia,
        China,
        India,
        Japan,
        Malaysia
    }
    enum Europe_Region
    {
        Sweden,
        Italia,
        UK,
        German,
        France
    }
    enum NorthAmerica_Region
    {
        Canada,
        USA
    }
    enum Oceania_Region
    {
        Newzealand,
        Australia
    }
    enum Korea_Airport
    {
        ICN_Incheon,
        CJU_Jeju
    }
    enum Russia_Airport
    {
        AER_Sochi,
        KZN_Kazan,
        CEK_Chelyabinsk,
        EGO_Belgorod
    }
    enum China_Airport
    {
        JJN_Jinjiang,
        KHN_Nanchang,
        PEK_Beijing
    }
    enum India_Airport
    {
        DEL_Deli,
        BOM_Mumbai,
        AMD_Ahmedabad,
        BBI_Bhubaneswar,
        BDQ_Vadodara
    }
    enum Japan_Airport
    {
        NRT_Narita,
        NGO_Chubu,
        KIX_Kansai,
        FUK_Fukuoka
    }
    enum Malaysia_Airport
    {
        KUL_KualaLumpur,
        BKI_KotaKinabalu
    }
    enum Sweden_Airport
    {
        ARN_Stockholm,
        GOT_Gothenburg
    }
    enum Italia_Airport
    {
        FCO_Rome,
        PSA_Pisa
    }
    enum UK_Airport
    {
        LHR_London,
        BFS_Belfast,
        GLA_Glasgow,
        STN_Stansted
    }
    enum German_Airport
    {
        MUC_Munich,
        FRA_Frankfurt,
        DUS_Dusseldorf
    }
    enum France_Airport
    {
        CDG_Paris,
        NCE_Nice,
        ORY_Orly
    }
    enum Canada_Airport
    {
        YYC_Calgary,
        YHZ_Halifax,
        YUL_Montreal,
        YOW_Ottawa,
        YVR_Vancouver
    }
    enum USA_Airport
    {
        JFK_Newyork,
        LAX_LA,
        BNA_Nashville,
        DTW_Detroit,
        MIA_Miami,
        PHX_Phoenix
    }
    enum Newzealand_Airport
    {
        AKL_Auckland
    }
    enum Australia_Airport
    {
        OOL_Goldcost
    }

    public Dropdown dropdown_C;
    public Dropdown dropdown_R;
    public Dropdown dropdown_A;

    private int select;
    private string before_airport_D = null;
    private string before_airport_A = null;
    void Start()
    {
        ContinentList();
        RegionList();
        AirportList();
    }
    void Update()
    {
        ContinentList();
        RegionList();
        AirportList();
    }
    void ContinentList()
    {
        dropdown_C.ClearOptions();
        string[] enumContinents = Enum.GetNames(typeof(Continents));
        List<string> Scontinents = new List<string>(enumContinents);
        if (GameObject.FindWithTag("C_Dropdown"))
        {
            dropdown_C.RefreshShownValue();
            dropdown_C.AddOptions(Scontinents);
            select = Dropdown_Region_Select(dropdown_C, select);
        }
        Scontinents.Clear();
    }
    void RegionList()
    {
        dropdown_R.ClearOptions();
        List<string> Sregions = new List<string>(Dropdown_Region(select));
        if (GameObject.FindWithTag("R_Dropdown"))
        {
            dropdown_R.AddOptions(Sregions);
            dropdown_R.RefreshShownValue();
            select = Dropdown_Airport_Select(dropdown_R, select);
        }
        Sregions.Clear();
    }
    void AirportList()
    {
        dropdown_A.ClearOptions();
        List<string> Sairport = new List<string>(Dropdown_Airport(select));
        if (GameObject.FindWithTag("A_Dropdown"))
        {
            dropdown_A.AddOptions(Sairport);
            dropdown_A.RefreshShownValue();
        }
        Sairport.Clear();
    }
    int Dropdown_Region_Select(Dropdown A, int select)
    {
        switch (A.value)
        {
            case 0:
                select = 100;
                break;
            case 1:
                select = 200;
                break;
            case 2:
                select = 300;
                break;
            case 3:
                select = 400;
                break;
        }
        return select;
    }
    static string[] Dropdown_Region(int select)
    {
        string[] enumRegions = new string[6];
        switch (select % 400)
        {
            case 100:
                enumRegions = Enum.GetNames(typeof(Asia_Region));
                break;
            case 200:
                enumRegions = Enum.GetNames(typeof(Europe_Region));
                break;
            case 300:
                enumRegions = Enum.GetNames(typeof(NorthAmerica_Region));
                break;
            case 0:
                enumRegions = Enum.GetNames(typeof(Oceania_Region));
                break;
        }
        return enumRegions;
    }
    int Dropdown_Airport_Select(Dropdown A, int select)
    {
        switch (A.value)
        {
            case 0:
                select += 10;
                break;
            case 1:
                select += 20;
                break;
            case 2:
                select += 30;
                break;
            case 3:
                select += 40;
                break;
            case 4:
                select += 50;
                break;
            case 5:
                select += 60;
                break;
            case 6:
                select += 70;
                break;
        }
        return select;
    }
    static string[] Dropdown_Airport(int select)
    {
        int Continent = select / 100 % 4;
        string[] enumAirport = new string[6];
        switch (select / 10 % 10)
        {
            case 1:
                if (Continent == 1)
                    enumAirport = Enum.GetNames(typeof(Korea_Airport));
                else if (Continent == 2)
                    enumAirport = Enum.GetNames(typeof(Sweden_Airport));
                else if (Continent == 3)
                    enumAirport = Enum.GetNames(typeof(Canada_Airport));
                else
                    enumAirport = Enum.GetNames(typeof(Newzealand_Airport));
                break;
            case 2:
                if (Continent == 1)
                    enumAirport = Enum.GetNames(typeof(Russia_Airport));
                else if (Continent == 2)
                    enumAirport = Enum.GetNames(typeof(Italia_Airport));
                else if (Continent == 3)
                    enumAirport = Enum.GetNames(typeof(USA_Airport));
                else
                    enumAirport = Enum.GetNames(typeof(Australia_Airport));
                break;
            case 3:
                if (Continent == 1)
                    enumAirport = Enum.GetNames(typeof(China_Airport));
                else
                    enumAirport = Enum.GetNames(typeof(UK_Airport));
                break;
            case 4:
                if (Continent == 1)
                    enumAirport = Enum.GetNames(typeof(India_Airport));
                else
                    enumAirport = Enum.GetNames(typeof(German_Airport));
                break;
            case 5:
                if (Continent == 1)
                    enumAirport = Enum.GetNames(typeof(Japan_Airport));
                else
                    enumAirport = Enum.GetNames(typeof(France_Airport));
                break;
            case 6:
                if (Continent == 1)
                    enumAirport = Enum.GetNames(typeof(Malaysia_Airport));
                break;
        }
        return enumAirport;
    }
    public void ButtonClick_DA()
    {
        GameObject before = GameObject.Find(before_airport_D);
        if(before_airport_D != null)
            if(before.GetComponent<MeshRenderer>().material.color == Color.red)
            {
                before.GetComponent<MeshRenderer>().material.color = Color.yellow;
            }
        string select_airport = dropdown_A.captionText.text;
        GameObject airport = GameObject.Find(select_airport);
        airport.GetComponent<MeshRenderer>().material.color = Color.red;

        before_airport_D = String.Empty;
        before_airport_D = String.Copy(select_airport);
    }
    public void ButtonClick_AA()
    {
        GameObject before = GameObject.Find(before_airport_A);
        if (before_airport_A != null)
            if (before.GetComponent<MeshRenderer>().material.color == Color.blue)
            {   
                before.GetComponent<MeshRenderer>().material.color = Color.yellow;
            }
        string select_airport = dropdown_A.captionText.text;
        GameObject airport = GameObject.Find(select_airport);
        airport.GetComponent<MeshRenderer>().material.color = Color.blue;

        before_airport_A = String.Empty;
        before_airport_A = String.Copy(select_airport);
    }
}
