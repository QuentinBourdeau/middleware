package fr.unice.polytech.BikeProject;

import generated.*;
import com.sun.xml.ws.fault.ServerSOAPFaultException;

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
                if(d.getProfile().getValue().contains("cycling")){
                    System.out.println("Grab a bike !");
                }
                if(d.getProfile().getValue().contains("foot")){
                    System.out.println("On foot !");
                }
                for (Step s : d.getSegment().getValue().getSteps().getValue().getStep()){
                    if(s.getDistance().intValue() == 0){
                        continue;
                    }
                    System.out.println("In " + s.getDistance().intValue() + " meters " + s.getInstruction().getValue());
                }
            }
            Map m = new Map();
            m.setGeoPosition(itinerary);
            m.draw();
            System.out.println("You have arrived at your destination!");}
        catch (ServerSOAPFaultException exception) {System.out.println(exception.getMessage());}
    }
}
