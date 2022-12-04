package fr.unice.polytech.BikeProject;

import com.sun.xml.ws.fault.ServerSOAPFaultException;
import generated.*;

import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        System.out.println("What is your starting adress?");
        String start = sc.nextLine();
        System.out.println("What is your destination?");
        String end = sc.nextLine();
        while (start.equals(end)){
            System.out.println("You're already here... both adresses are the same");
            System.out.println("What is your starting adress?");
            start = sc.nextLine();
            System.out.println("What is your destination?");
            end = sc.nextLine();
        }

        Biking applicationService = new Biking();
        IBiking service = applicationService.getBasicHttpBindingIBiking();
        try{
            Itinerary itinerary = service.getItinerary(start, end);
            for (Direction d : itinerary.getDirections().getValue().getDirection()) {
                System.out.println(d.getProfile().getValue());
                for (Step s : d.getSegment().getValue().getSteps().getValue().getStep()){
                    System.out.println("In " + s.getDistance().intValue() + " meters " + s.getInstruction().getValue());
                }
            }
            System.out.println("You have arrived at your destination!");}
        catch (ServerSOAPFaultException exception) {System.out.println(exception.getMessage());}
    }
}
