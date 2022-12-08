package fr.unice.polytech.BikeProject;

import generated.*;
import com.sun.xml.ws.fault.ServerSOAPFaultException;

import java.text.DecimalFormat;
import java.util.Scanner;

public class Main {
    private static final DecimalFormat df = new DecimalFormat("0.00");
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        Biking applicationService = new Biking();
        IBiking service = applicationService.getBasicHttpBindingIBiking();
        while (true) {
            System.out.println("What is your starting adress?");
            String start = sc.nextLine();
            System.out.println("What is your destination?");
            String end = sc.nextLine();
            try{
                Itinerary itinerary = service.getItinerary(start, end);
                if(itinerary.getError().getValue().contains("Jesus")){
                    System.out.println("it might be a good idea to avoid trying to cross the seas on a bike");
                    continue;
                }
                if(itinerary.getError().getValue().contains("wrong address")){
                    System.out.println("One of the given address was impossible to locate");
                    continue;
                }

                if(itinerary.getError().getValue().contains("FullFoot")){
                    System.out.println("Bikes aren't that usefull to get to your destination, so here's a way to get there on foot");
                    try {
                        Thread.sleep(1500);
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                }
                if(itinerary.getDuration() > 3600){
                    System.out.println("Be Careful, you're going for a long trip");
                    System.out.println("approx time : " + df.format(itinerary.getDuration()/3600) + "hours");
                    try {
                        Thread.sleep(4000);
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                }

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
            break;
        }




    }
}
