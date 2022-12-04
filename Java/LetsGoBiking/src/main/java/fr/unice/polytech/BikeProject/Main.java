package fr.unice.polytech.BikeProject;

import com.sun.xml.ws.fault.ServerSOAPFaultException;
import generated.*;

import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        System.out.println("oui");
        Scanner sc = new Scanner(System.in);
        System.out.println("Rentrez l'adresse de votre point de départ : ");
        String origin = sc.nextLine();
        System.out.println("Rentrez l'adresse de votre point d'arrivée : ");
        String destination = sc.nextLine();
        while (origin.equals(destination)){
            System.out.println("Vos adresse de départ et d'arrivée sont les mêmes!");
            System.out.println("Rentrez l'adresse de votre point de départ : ");
            origin = sc.nextLine();
            System.out.println("Rentrez l'adresse de votre point d'arrivée : ");
            destination = sc.nextLine();
        }

        Biking applicationService = new Biking();
        IBiking service = applicationService.getBasicHttpBindingIBiking();
        try{
            Itinerary itinerary = service.getItinerary(origin, destination);
            for (Direction d : itinerary.getDirections().getValue().getDirection()) {
                System.out.println(d.getProfile().getValue());
                for (Step s : d.getSegment().getValue().getSteps().getValue().getStep()){
                    System.out.println("In " + s.getDistance().intValue() + " meters " + s.getInstruction().getValue());
                }
            }
            System.out.println("You have arrived!");}
        catch (ServerSOAPFaultException exception) {System.out.println(exception.getMessage());}
    }
}
